using SupportTicketManagement.Application.Comments;
using SupportTicketManagement.Application.Common;
using SupportTicketManagement.Application.Interfaces;
using SupportTicketManagement.Application.Tickets;
using SupportTicketManagement.Domain.Entities;

namespace SupportTicketManagement.Application.Comments;

public sealed class CommentService : ICommentService
{
    private const int MessageMaxLength = 4000;

    private readonly ICommentRepository _commentRepository;
    private readonly ITicketRepository _ticketRepository;
    private readonly IUserRepository _userRepository;

    public CommentService(
        ICommentRepository commentRepository,
        ITicketRepository ticketRepository,
        IUserRepository userRepository)
    {
        _commentRepository = commentRepository;
        _ticketRepository = ticketRepository;
        _userRepository = userRepository;
    }

    public async Task<ServiceResult<CommentDto>> AddAsync(
        int ticketId,
        CreateCommentRequest request,
        CancellationToken cancellationToken = default)
    {
        var ticket = await _ticketRepository.GetByIdAsync(ticketId, cancellationToken);
        if (ticket is null)
        {
            return ServiceResult<CommentDto>.NotFound($"Ticket with id {ticketId} not found.");
        }

        if (!await _userRepository.ExistsAsync(request.CreatedById, cancellationToken))
        {
            return ServiceResult<CommentDto>.NotFound($"User with id {request.CreatedById} not found.");
        }

        var validationErrors = ValidateMessage(request.Message);
        if (validationErrors.Count > 0)
        {
            return ServiceResult<CommentDto>.ValidationFailure(validationErrors);
        }

        var comment = new Comment
        {
            TicketId = ticketId,
            Message = request.Message.Trim(),
            CreatedById = request.CreatedById,
            CreatedAt = DateTime.UtcNow
        };

        await _commentRepository.AddAsync(comment, cancellationToken);
        await _commentRepository.SaveChangesAsync(cancellationToken);

        var author = await _userRepository.GetByIdAsync(request.CreatedById, cancellationToken);
        comment.CreatedBy = author!;

        return ServiceResult<CommentDto>.Success(TicketDtoMapper.ToComment(comment));
    }

    private static List<ValidationError> ValidateMessage(string message)
    {
        var errors = new List<ValidationError>();

        if (string.IsNullOrWhiteSpace(message))
        {
            errors.Add(new ValidationError("message", "Message is required."));
        }
        else if (message.Trim().Length > MessageMaxLength)
        {
            errors.Add(new ValidationError("message", $"Message must not exceed {MessageMaxLength} characters."));
        }

        return errors;
    }
}

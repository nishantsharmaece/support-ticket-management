using SupportTicketManagement.Application.Common;
using SupportTicketManagement.Application.Interfaces;
using SupportTicketManagement.Application.Tickets;
using SupportTicketManagement.Domain.Entities;
using SupportTicketManagement.Domain.Enums;
using SupportTicketManagement.Domain.Services;

namespace SupportTicketManagement.Application.Tickets;

public sealed class TicketService : ITicketService
{
    private const int TitleMaxLength = 200;
    private const int DescriptionMaxLength = 4000;

    private readonly ITicketRepository _ticketRepository;
    private readonly IUserRepository _userRepository;

    public TicketService(ITicketRepository ticketRepository, IUserRepository userRepository)
    {
        _ticketRepository = ticketRepository;
        _userRepository = userRepository;
    }

    public async Task<ServiceResult<TicketDetailDto>> CreateAsync(
        CreateTicketRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!await _userRepository.ExistsAsync(request.AssignedToId, cancellationToken))
        {
            return ServiceResult<TicketDetailDto>.NotFound($"User with id {request.AssignedToId} not found.");
        }

        if (!await _userRepository.ExistsAsync(request.CreatedById, cancellationToken))
        {
            return ServiceResult<TicketDetailDto>.NotFound($"User with id {request.CreatedById} not found.");
        }

        var validationErrors = ValidateTicketFields(request.Title, request.Description, request.Priority);
        if (validationErrors.Count > 0)
        {
            return ServiceResult<TicketDetailDto>.ValidationFailure(validationErrors);
        }

        var now = DateTime.UtcNow;
        var ticket = new Ticket
        {
            Title = request.Title.Trim(),
            Description = request.Description?.Trim() ?? string.Empty,
            Priority = request.Priority,
            Status = TicketStatus.Open,
            AssignedToId = request.AssignedToId,
            CreatedById = request.CreatedById,
            CreatedAt = now,
            UpdatedAt = now
        };

        await _ticketRepository.AddAsync(ticket, cancellationToken);
        await _ticketRepository.SaveChangesAsync(cancellationToken);

        var createdTicket = await _ticketRepository.GetByIdWithDetailsAsync(ticket.Id, cancellationToken);
        return ServiceResult<TicketDetailDto>.Success(TicketDtoMapper.ToDetail(createdTicket!));
    }

    public async Task<ServiceResult<TicketListResponseDto>> ListAsync(
        TicketListQuery query,
        CancellationToken cancellationToken = default)
    {
        var search = string.IsNullOrWhiteSpace(query.Search) ? null : query.Search.Trim();
        var tickets = await _ticketRepository.ListAsync(search, query.Status, cancellationToken);

        return ServiceResult<TicketListResponseDto>.Success(new TicketListResponseDto
        {
            Items = tickets.Select(TicketDtoMapper.ToSummary).ToList()
        });
    }

    public async Task<ServiceResult<TicketDetailDto>> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var ticket = await _ticketRepository.GetByIdWithDetailsAsync(id, cancellationToken);
        if (ticket is null)
        {
            return ServiceResult<TicketDetailDto>.NotFound($"Ticket with id {id} not found.");
        }

        return ServiceResult<TicketDetailDto>.Success(TicketDtoMapper.ToDetail(ticket));
    }

    public async Task<ServiceResult<TicketDetailDto>> UpdateAsync(
        int id,
        UpdateTicketRequest request,
        CancellationToken cancellationToken = default)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id, cancellationToken);
        if (ticket is null)
        {
            return ServiceResult<TicketDetailDto>.NotFound($"Ticket with id {id} not found.");
        }

        if (!await _userRepository.ExistsAsync(request.AssignedToId, cancellationToken))
        {
            return ServiceResult<TicketDetailDto>.NotFound($"User with id {request.AssignedToId} not found.");
        }

        var validationErrors = ValidateTicketFields(request.Title, request.Description, request.Priority);
        if (validationErrors.Count > 0)
        {
            return ServiceResult<TicketDetailDto>.ValidationFailure(validationErrors);
        }

        ticket.Title = request.Title.Trim();
        ticket.Description = request.Description?.Trim() ?? string.Empty;
        ticket.Priority = request.Priority;
        ticket.AssignedToId = request.AssignedToId;
        ticket.UpdatedAt = DateTime.UtcNow;

        await _ticketRepository.UpdateAsync(ticket, cancellationToken);
        await _ticketRepository.SaveChangesAsync(cancellationToken);

        var updatedTicket = await _ticketRepository.GetByIdWithDetailsAsync(id, cancellationToken);
        return ServiceResult<TicketDetailDto>.Success(TicketDtoMapper.ToDetail(updatedTicket!));
    }

    public async Task<ServiceResult<TicketDetailDto>> ChangeStatusAsync(
        int id,
        ChangeTicketStatusRequest request,
        CancellationToken cancellationToken = default)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id, cancellationToken);
        if (ticket is null)
        {
            return ServiceResult<TicketDetailDto>.NotFound($"Ticket with id {id} not found.");
        }

        if (!Enum.IsDefined(request.Status))
        {
            return ServiceResult<TicketDetailDto>.ValidationFailure(
                [new ValidationError("status", "Invalid status value.")]);
        }

        if (!TicketStatusStateMachine.CanTransition(ticket.Status, request.Status))
        {
            return ServiceResult<TicketDetailDto>.Conflict(
                $"Cannot transition from {ticket.Status} to {request.Status}.");
        }

        ticket.Status = request.Status;
        ticket.UpdatedAt = DateTime.UtcNow;

        await _ticketRepository.UpdateAsync(ticket, cancellationToken);
        await _ticketRepository.SaveChangesAsync(cancellationToken);

        var updatedTicket = await _ticketRepository.GetByIdWithDetailsAsync(id, cancellationToken);
        return ServiceResult<TicketDetailDto>.Success(TicketDtoMapper.ToDetail(updatedTicket!));
    }

    private static List<ValidationError> ValidateTicketFields(
        string title,
        string? description,
        Priority priority)
    {
        var errors = new List<ValidationError>();

        if (string.IsNullOrWhiteSpace(title))
        {
            errors.Add(new ValidationError("title", "Title is required."));
        }
        else if (title.Trim().Length > TitleMaxLength)
        {
            errors.Add(new ValidationError("title", $"Title must not exceed {TitleMaxLength} characters."));
        }

        if (description is not null && description.Trim().Length > DescriptionMaxLength)
        {
            errors.Add(new ValidationError("description", $"Description must not exceed {DescriptionMaxLength} characters."));
        }

        if (!Enum.IsDefined(priority))
        {
            errors.Add(new ValidationError("priority", "Invalid priority value."));
        }

        return errors;
    }
}

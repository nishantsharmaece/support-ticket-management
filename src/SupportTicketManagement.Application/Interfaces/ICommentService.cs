using SupportTicketManagement.Application.Comments;
using SupportTicketManagement.Application.Common;

namespace SupportTicketManagement.Application.Interfaces;

public interface ICommentService
{
    Task<ServiceResult<CommentDto>> AddAsync(
        int ticketId,
        CreateCommentRequest request,
        CancellationToken cancellationToken = default);
}

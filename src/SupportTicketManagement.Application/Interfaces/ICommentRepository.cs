using SupportTicketManagement.Domain.Entities;

namespace SupportTicketManagement.Application.Interfaces;

public interface ICommentRepository
{
    Task AddAsync(Comment comment, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}

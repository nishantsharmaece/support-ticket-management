using SupportTicketManagement.Domain.Entities;
using SupportTicketManagement.Domain.Enums;

namespace SupportTicketManagement.Application.Interfaces;

public interface ITicketRepository
{
    Task<Ticket?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<Ticket?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Ticket>> ListAsync(
        string? search,
        TicketStatus? status,
        CancellationToken cancellationToken = default);

    Task AddAsync(Ticket ticket, CancellationToken cancellationToken = default);

    Task UpdateAsync(Ticket ticket, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}

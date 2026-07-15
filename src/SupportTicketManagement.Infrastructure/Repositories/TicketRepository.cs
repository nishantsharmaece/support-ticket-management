using Microsoft.EntityFrameworkCore;
using SupportTicketManagement.Application.Interfaces;
using SupportTicketManagement.Domain.Entities;
using SupportTicketManagement.Domain.Enums;
using SupportTicketManagement.Infrastructure.Persistence;

namespace SupportTicketManagement.Infrastructure.Repositories;

public sealed class TicketRepository : ITicketRepository
{
    private readonly AppDbContext _context;

    public TicketRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Ticket?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Tickets
            .FirstOrDefaultAsync(ticket => ticket.Id == id, cancellationToken);
    }

    public async Task<Ticket?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Tickets
            .AsNoTracking()
            .Include(ticket => ticket.AssignedTo)
            .Include(ticket => ticket.CreatedBy)
            .Include(ticket => ticket.Comments)
                .ThenInclude(comment => comment.CreatedBy)
            .FirstOrDefaultAsync(ticket => ticket.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Ticket>> ListAsync(
        string? search,
        TicketStatus? status,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Tickets
            .AsNoTracking()
            .Include(ticket => ticket.AssignedTo)
            .AsQueryable();

        if (status.HasValue)
        {
            query = query.Where(ticket => ticket.Status == status.Value);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            var normalizedSearch = search.Trim().ToLowerInvariant();
            query = query.Where(ticket =>
                ticket.Title.ToLower().Contains(normalizedSearch) ||
                ticket.Description.ToLower().Contains(normalizedSearch));
        }

        return await query
            .OrderByDescending(ticket => ticket.UpdatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Ticket ticket, CancellationToken cancellationToken = default)
    {
        await _context.Tickets.AddAsync(ticket, cancellationToken);
    }

    public Task UpdateAsync(Ticket ticket, CancellationToken cancellationToken = default)
    {
        _context.Tickets.Update(ticket);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}

using Microsoft.EntityFrameworkCore;
using SupportTicketManagement.Application.Interfaces;
using SupportTicketManagement.Domain.Entities;
using SupportTicketManagement.Infrastructure.Persistence;

namespace SupportTicketManagement.Infrastructure.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.TicketUsers
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.TicketUsers
            .AsNoTracking()
            .OrderBy(user => user.Name)
            .ToListAsync(cancellationToken);
    }

    public Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return _context.TicketUsers.AnyAsync(user => user.Id == id, cancellationToken);
    }
}

using SupportTicketManagement.Application.Interfaces;
using SupportTicketManagement.Domain.Entities;
using SupportTicketManagement.Infrastructure.Persistence;

namespace SupportTicketManagement.Infrastructure.Repositories;

public sealed class CommentRepository : ICommentRepository
{
    private readonly AppDbContext _context;

    public CommentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Comment comment, CancellationToken cancellationToken = default)
    {
        await _context.Comments.AddAsync(comment, cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}

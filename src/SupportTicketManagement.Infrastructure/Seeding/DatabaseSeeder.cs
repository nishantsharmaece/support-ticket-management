using Microsoft.EntityFrameworkCore;
using SupportTicketManagement.Domain.Entities;
using SupportTicketManagement.Domain.Enums;
using SupportTicketManagement.Infrastructure.Persistence;

namespace SupportTicketManagement.Infrastructure.Seeding;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(AppDbContext context, CancellationToken cancellationToken = default)
    {
        if (await context.TicketUsers.AnyAsync(cancellationToken))
        {
            return;
        }

        var users = new[]
        {
            new User { Name = "Jane Agent", Email = "jane@example.com", Role = "Agent" },
            new User { Name = "John Manager", Email = "john@example.com", Role = "Manager" },
            new User { Name = "Alice Agent", Email = "alice@example.com", Role = "Agent" },
            new User { Name = "Bob Agent", Email = "bob@example.com", Role = "Agent" },
            new User { Name = "Carol Manager", Email = "carol@example.com", Role = "Manager" }
        };

        context.TicketUsers.AddRange(users);
        await context.SaveChangesAsync(cancellationToken);

        if (await context.Tickets.AnyAsync(cancellationToken))
        {
            return;
        }

        var now = DateTime.UtcNow;
        var jane = users[0];
        var john = users[1];
        var alice = users[2];
        var bob = users[3];
        var carol = users[4];

        var tickets = new[]
        {
            new Ticket
            {
                Title = "Cannot log in to portal",
                Description = "User reports invalid credentials after password reset.",
                Priority = Priority.High,
                Status = TicketStatus.Open,
                AssignedToId = jane.Id,
                CreatedById = john.Id,
                CreatedAt = now.AddDays(-2),
                UpdatedAt = now.AddDays(-2)
            },
            new Ticket
            {
                Title = "Printer not responding",
                Description = "Office printer on floor 2 shows offline status.",
                Priority = Priority.Medium,
                Status = TicketStatus.InProgress,
                AssignedToId = alice.Id,
                CreatedById = jane.Id,
                CreatedAt = now.AddDays(-1),
                UpdatedAt = now.AddHours(-3)
            },
            new Ticket
            {
                Title = "Email sync delay",
                Description = "Mailbox sync is delayed by several hours.",
                Priority = Priority.Low,
                Status = TicketStatus.Resolved,
                AssignedToId = bob.Id,
                CreatedById = carol.Id,
                CreatedAt = now.AddDays(-5),
                UpdatedAt = now.AddDays(-1)
            }
        };

        context.Tickets.AddRange(tickets);
        await context.SaveChangesAsync(cancellationToken);

        var comments = new[]
        {
            new Comment
            {
                TicketId = tickets[0].Id,
                Message = "Confirmed password reset email was delivered.",
                CreatedById = jane.Id,
                CreatedAt = now.AddDays(-1)
            },
            new Comment
            {
                TicketId = tickets[1].Id,
                Message = "Restarted print spooler and checked network cable.",
                CreatedById = alice.Id,
                CreatedAt = now.AddHours(-2)
            }
        };

        context.Comments.AddRange(comments);
        await context.SaveChangesAsync(cancellationToken);
    }
}

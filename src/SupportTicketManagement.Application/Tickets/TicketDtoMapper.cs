using SupportTicketManagement.Application.Comments;
using SupportTicketManagement.Application.Tickets;
using SupportTicketManagement.Application.Users;
using SupportTicketManagement.Domain.Entities;

namespace SupportTicketManagement.Application.Tickets;

internal static class TicketDtoMapper
{
    public static TicketSummaryDto ToSummary(Ticket ticket)
    {
        return new TicketSummaryDto
        {
            Id = ticket.Id,
            Title = ticket.Title,
            Priority = ticket.Priority.ToString(),
            Status = ticket.Status.ToString(),
            AssignedTo = ToUserSummary(ticket.AssignedTo),
            CreatedAt = FormatUtc(ticket.CreatedAt),
            UpdatedAt = FormatUtc(ticket.UpdatedAt)
        };
    }

    public static TicketDetailDto ToDetail(Ticket ticket)
    {
        return new TicketDetailDto
        {
            Id = ticket.Id,
            Title = ticket.Title,
            Description = ticket.Description,
            Priority = ticket.Priority.ToString(),
            Status = ticket.Status.ToString(),
            AssignedTo = ToUserSummary(ticket.AssignedTo),
            CreatedBy = ToUserSummary(ticket.CreatedBy),
            CreatedAt = FormatUtc(ticket.CreatedAt),
            UpdatedAt = FormatUtc(ticket.UpdatedAt),
            Comments = ticket.Comments
                .OrderBy(comment => comment.CreatedAt)
                .Select(ToComment)
                .ToList()
        };
    }

    public static CommentDto ToComment(Comment comment)
    {
        return new CommentDto
        {
            Id = comment.Id,
            Message = comment.Message,
            CreatedBy = ToUserSummary(comment.CreatedBy),
            CreatedAt = FormatUtc(comment.CreatedAt)
        };
    }

    public static UserSummaryDto ToUserSummary(User user)
    {
        return new UserSummaryDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role
        };
    }

    private static string FormatUtc(DateTime value)
    {
        return DateTime.SpecifyKind(value, DateTimeKind.Utc).ToString("o");
    }
}

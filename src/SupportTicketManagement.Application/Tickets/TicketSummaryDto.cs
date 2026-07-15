using SupportTicketManagement.Application.Users;

namespace SupportTicketManagement.Application.Tickets;

public sealed class TicketSummaryDto
{
    public int Id { get; init; }

    public string Title { get; init; } = string.Empty;

    public string Priority { get; init; } = string.Empty;

    public string Status { get; init; } = string.Empty;

    public UserSummaryDto AssignedTo { get; init; } = null!;

    public string CreatedAt { get; init; } = string.Empty;

    public string UpdatedAt { get; init; } = string.Empty;
}

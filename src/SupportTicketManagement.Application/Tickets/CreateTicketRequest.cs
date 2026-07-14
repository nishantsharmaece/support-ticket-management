using SupportTicketManagement.Domain.Enums;

namespace SupportTicketManagement.Application.Tickets;

public sealed class CreateTicketRequest
{
    public string Title { get; init; } = string.Empty;

    public string? Description { get; init; }

    public Priority Priority { get; init; }

    public int AssignedToId { get; init; }

    public int CreatedById { get; init; }
}

using SupportTicketManagement.Domain.Enums;

namespace SupportTicketManagement.Application.Tickets;

public sealed class UpdateTicketRequest
{
    public string Title { get; init; } = string.Empty;

    public string? Description { get; init; }

    public Priority Priority { get; init; }

    public int AssignedToId { get; init; }
}

using SupportTicketManagement.Domain.Enums;

namespace SupportTicketManagement.Application.Tickets;

public sealed class ChangeTicketStatusRequest
{
    public TicketStatus Status { get; init; }
}

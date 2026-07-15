using SupportTicketManagement.Domain.Enums;

namespace SupportTicketManagement.Application.Common;

public sealed class TicketListQuery
{
    public string? Search { get; init; }

    public TicketStatus? Status { get; init; }
}

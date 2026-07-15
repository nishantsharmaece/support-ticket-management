namespace SupportTicketManagement.Application.Tickets;

public sealed class TicketListResponseDto
{
    public IReadOnlyList<TicketSummaryDto> Items { get; init; } = Array.Empty<TicketSummaryDto>();
}

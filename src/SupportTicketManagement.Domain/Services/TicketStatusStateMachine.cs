using SupportTicketManagement.Domain.Enums;

namespace SupportTicketManagement.Domain.Services;

public static class TicketStatusStateMachine
{
    private static readonly IReadOnlyDictionary<TicketStatus, TicketStatus[]> AllowedTransitions =
        new Dictionary<TicketStatus, TicketStatus[]>
        {
            [TicketStatus.Open] = [TicketStatus.InProgress, TicketStatus.Cancelled],
            [TicketStatus.InProgress] = [TicketStatus.Resolved, TicketStatus.Cancelled],
            [TicketStatus.Resolved] = [TicketStatus.Closed],
            [TicketStatus.Closed] = [],
            [TicketStatus.Cancelled] = []
        };

    public static bool CanTransition(TicketStatus currentStatus, TicketStatus requestedStatus)
    {
        return AllowedTransitions[currentStatus].Contains(requestedStatus);
    }

    public static IReadOnlyCollection<TicketStatus> GetAllowedTransitions(TicketStatus currentStatus)
    {
        return AllowedTransitions[currentStatus];
    }

    public static bool IsTerminal(TicketStatus status)
    {
        return status is TicketStatus.Closed or TicketStatus.Cancelled;
    }
}

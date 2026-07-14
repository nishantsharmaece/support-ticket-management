using SupportTicketManagement.Domain.Enums;
using SupportTicketManagement.Domain.Services;

namespace SupportTicketManagement.Application.Common;

public static class StatusTransitionHelper
{
    private static IReadOnlyCollection<TicketStatus> GetAllowedTransitions(TicketStatus currentStatus)
    {
        return TicketStatusStateMachine.GetAllowedTransitions(currentStatus);
    }

    public static bool IsTerminal(TicketStatus status)
    {
        return TicketStatusStateMachine.IsTerminal(status);
    }

    public static IReadOnlyCollection<string> GetAllowedTransitionNames(string currentStatus)
    {
        if (!TicketEnumParser.TryParseStatus(currentStatus, out var status))
        {
            return Array.Empty<string>();
        }

        return GetAllowedTransitions(status)
            .Select(value => value.ToString())
            .ToList();
    }

    public static bool IsTerminalStatus(string currentStatus)
    {
        return TicketEnumParser.TryParseStatus(currentStatus, out var status) && IsTerminal(status);
    }
}

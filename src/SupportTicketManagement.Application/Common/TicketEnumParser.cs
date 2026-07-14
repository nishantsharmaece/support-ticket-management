using SupportTicketManagement.Domain.Enums;

namespace SupportTicketManagement.Application.Common;

public static class TicketEnumParser
{
    public static bool TryParsePriority(string? value, out Priority priority)
    {
        priority = default;
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return Enum.TryParse(value, ignoreCase: true, out priority) && Enum.IsDefined(priority);
    }

    public static bool TryParseStatus(string? value, out TicketStatus status)
    {
        status = default;
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return Enum.TryParse(value, ignoreCase: true, out status) && Enum.IsDefined(status);
    }

    public static TicketListQuery CreateListQuery(string? search, string? statusFilter)
    {
        TicketStatus? status = null;
        if (!string.IsNullOrWhiteSpace(statusFilter) && TryParseStatus(statusFilter, out var parsedStatus))
        {
            status = parsedStatus;
        }

        return new TicketListQuery
        {
            Search = search,
            Status = status
        };
    }
}

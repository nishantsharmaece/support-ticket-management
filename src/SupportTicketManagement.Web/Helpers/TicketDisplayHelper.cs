namespace SupportTicketManagement.Web.Helpers;

public static class TicketDisplayHelper
{
    public static string FormatStatus(string status)
    {
        return status switch
        {
            "InProgress" => "In Progress",
            _ => status
        };
    }

    public static string StatusBadgeClass(string status)
    {
        return status switch
        {
            "Open" => "bg-primary",
            "InProgress" => "bg-info text-dark",
            "Resolved" => "bg-warning text-dark",
            "Closed" => "bg-secondary",
            "Cancelled" => "bg-dark",
            _ => "bg-light text-dark"
        };
    }

    public static string PriorityBadgeClass(string priority)
    {
        return priority switch
        {
            "High" => "bg-danger",
            "Medium" => "bg-warning text-dark",
            "Low" => "bg-secondary",
            _ => "bg-light text-dark"
        };
    }

    public static string FormatTimestamp(string isoTimestamp)
    {
        if (DateTime.TryParse(isoTimestamp, null, System.Globalization.DateTimeStyles.RoundtripKind, out var value))
        {
            return value.ToUniversalTime().ToString("yyyy-MM-dd HH:mm") + " UTC";
        }

        return isoTimestamp;
    }
}

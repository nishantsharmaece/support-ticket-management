using SupportTicketManagement.Domain.Enums;

namespace SupportTicketManagement.Domain.Exceptions;

public class InvalidTicketStatusTransitionException : Exception
{
    public InvalidTicketStatusTransitionException(TicketStatus currentStatus, TicketStatus requestedStatus)
        : base($"Cannot transition from {currentStatus} to {requestedStatus}.")
    {
        CurrentStatus = currentStatus;
        RequestedStatus = requestedStatus;
    }

    public TicketStatus CurrentStatus { get; }

    public TicketStatus RequestedStatus { get; }
}

namespace SupportTicketManagement.Application.Common;

public sealed class ValidationError
{
    public ValidationError(string field, string message)
    {
        Field = field;
        Message = message;
    }

    public string Field { get; }

    public string Message { get; }
}

namespace SupportTicketManagement.Application.Comments;

public sealed class CreateCommentRequest
{
    public string Message { get; init; } = string.Empty;

    public int CreatedById { get; init; }
}

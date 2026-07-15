using SupportTicketManagement.Application.Users;

namespace SupportTicketManagement.Application.Comments;

public sealed class CommentDto
{
    public int Id { get; init; }

    public string Message { get; init; } = string.Empty;

    public UserSummaryDto CreatedBy { get; init; } = null!;

    public string CreatedAt { get; init; } = string.Empty;
}

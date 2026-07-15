using SupportTicketManagement.Application.Comments;
using SupportTicketManagement.Application.Users;

namespace SupportTicketManagement.Application.Tickets;

public sealed class TicketDetailDto
{
    public int Id { get; init; }

    public string Title { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public string Priority { get; init; } = string.Empty;

    public string Status { get; init; } = string.Empty;

    public UserSummaryDto AssignedTo { get; init; } = null!;

    public UserSummaryDto CreatedBy { get; init; } = null!;

    public string CreatedAt { get; init; } = string.Empty;

    public string UpdatedAt { get; init; } = string.Empty;

    public IReadOnlyList<CommentDto> Comments { get; init; } = Array.Empty<CommentDto>();
}

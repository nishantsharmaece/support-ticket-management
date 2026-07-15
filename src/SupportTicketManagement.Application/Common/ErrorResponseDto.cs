namespace SupportTicketManagement.Application.Common;

public sealed class ErrorResponseDto
{
    public string Title { get; init; } = string.Empty;

    public int Status { get; init; }

    public IReadOnlyList<ValidationError> Errors { get; init; } = Array.Empty<ValidationError>();
}

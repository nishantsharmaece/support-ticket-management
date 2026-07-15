using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SupportTicketManagement.Web.ViewModels;

public sealed class TicketDetailsViewModel
{
    public TicketDetailViewModel Ticket { get; set; } = new();

    public ChangeStatusViewModel StatusForm { get; set; } = new();

    public AddCommentViewModel CommentForm { get; set; } = new();

    public string? SuccessMessage { get; set; }

    public string? ErrorMessage { get; set; }
}

public sealed class TicketDetailViewModel
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Priority { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string AssigneeName { get; set; } = string.Empty;

    public string CreatorName { get; set; } = string.Empty;

    public string CreatedAt { get; set; } = string.Empty;

    public string UpdatedAt { get; set; } = string.Empty;

    public IReadOnlyList<CommentItemViewModel> Comments { get; set; } = Array.Empty<CommentItemViewModel>();
}

public sealed class CommentItemViewModel
{
    public int Id { get; set; }

    public string Message { get; set; } = string.Empty;

    public string AuthorName { get; set; } = string.Empty;

    public string CreatedAt { get; set; } = string.Empty;
}

public sealed class ChangeStatusViewModel
{
    [Required(ErrorMessage = "Please select a status.")]
    [Display(Name = "New Status")]
    public string? NewStatus { get; set; }

    public IReadOnlyList<SelectListItem> AllowedStatusOptions { get; set; } = Array.Empty<SelectListItem>();

    public bool IsTerminal { get; set; }
}

public sealed class AddCommentViewModel
{
    [Required(ErrorMessage = "Message is required.")]
    [MaxLength(4000, ErrorMessage = "Message must not exceed 4000 characters.")]
    [Display(Name = "Message")]
    public string Message { get; set; } = string.Empty;

    [Required(ErrorMessage = "Creator is required.")]
    [Display(Name = "Creator")]
    public int? CreatedById { get; set; }

    public IReadOnlyList<SelectListItem> UserOptions { get; set; } = Array.Empty<SelectListItem>();
}

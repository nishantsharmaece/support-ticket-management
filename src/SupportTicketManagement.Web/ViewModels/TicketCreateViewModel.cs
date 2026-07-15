using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SupportTicketManagement.Web.ViewModels;

public sealed class TicketCreateViewModel
{
    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(200, ErrorMessage = "Title must not exceed 200 characters.")]
    [Display(Name = "Title")]
    public string Title { get; set; } = string.Empty;

    [MaxLength(4000, ErrorMessage = "Description must not exceed 4000 characters.")]
    [Display(Name = "Description")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Priority is required.")]
    [Display(Name = "Priority")]
    public string? Priority { get; set; }

    [Required(ErrorMessage = "Assignee is required.")]
    [Display(Name = "Assignee")]
    public int? AssignedToId { get; set; }

    [Required(ErrorMessage = "Creator is required.")]
    [Display(Name = "Creator")]
    public int? CreatedById { get; set; }

    public IReadOnlyList<SelectListItem> PriorityOptions { get; set; } = Array.Empty<SelectListItem>();

    public IReadOnlyList<SelectListItem> UserOptions { get; set; } = Array.Empty<SelectListItem>();
}

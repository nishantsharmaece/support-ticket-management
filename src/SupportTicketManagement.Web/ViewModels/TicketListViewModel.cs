using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SupportTicketManagement.Web.ViewModels;

public sealed class TicketListViewModel
{
    public string? Search { get; set; }

    [Display(Name = "Status")]
    public string? StatusFilter { get; set; }

    public IReadOnlyList<TicketListItemViewModel> Tickets { get; set; } = Array.Empty<TicketListItemViewModel>();

    public IReadOnlyList<SelectListItem> StatusOptions { get; set; } = Array.Empty<SelectListItem>();

    public bool HasActiveFilters { get; set; }
}

public sealed class TicketListItemViewModel
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string Priority { get; set; } = string.Empty;

    public string AssigneeName { get; set; } = string.Empty;

    public string UpdatedAt { get; set; } = string.Empty;
}

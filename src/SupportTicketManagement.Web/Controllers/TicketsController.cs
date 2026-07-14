using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SupportTicketManagement.Application.Comments;
using SupportTicketManagement.Application.Common;
using SupportTicketManagement.Application.Interfaces;
using SupportTicketManagement.Application.Tickets;
using SupportTicketManagement.Web.Helpers;
using SupportTicketManagement.Web.ViewModels;

namespace SupportTicketManagement.Web.Controllers;

public sealed class TicketsController : Controller
{
    private readonly ITicketService _ticketService;
    private readonly ICommentService _commentService;
    private readonly IUserService _userService;

    public TicketsController(
        ITicketService ticketService,
        ICommentService commentService,
        IUserService userService)
    {
        _ticketService = ticketService;
        _commentService = commentService;
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? search, string? statusFilter, CancellationToken cancellationToken)
    {
        string? parsedStatusFilter = null;
        if (!string.IsNullOrWhiteSpace(statusFilter))
        {
            if (!TicketEnumParser.TryParseStatus(statusFilter, out _))
            {
                ModelState.AddModelError(nameof(TicketListViewModel.StatusFilter), "Invalid status value.");
            }
            else
            {
                parsedStatusFilter = statusFilter;
            }
        }

        if (!ModelState.IsValid)
        {
            return View(new TicketListViewModel
            {
                Search = search,
                StatusFilter = statusFilter,
                StatusOptions = BuildStatusFilterOptions(statusFilter)
            });
        }

        var result = await _ticketService.ListAsync(
            TicketEnumParser.CreateListQuery(search, parsedStatusFilter),
            cancellationToken);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "Unable to load tickets.");
            return View(new TicketListViewModel
            {
                Search = search,
                StatusFilter = statusFilter,
                StatusOptions = BuildStatusFilterOptions(statusFilter)
            });
        }

        var viewModel = new TicketListViewModel
        {
            Search = search,
            StatusFilter = statusFilter,
            HasActiveFilters = !string.IsNullOrWhiteSpace(search) || !string.IsNullOrWhiteSpace(statusFilter),
            StatusOptions = BuildStatusFilterOptions(statusFilter),
            Tickets = result.Value!.Items.Select(item => new TicketListItemViewModel
            {
                Id = item.Id,
                Title = item.Title,
                Status = item.Status,
                Priority = item.Priority,
                AssigneeName = item.AssignedTo.Name,
                UpdatedAt = item.UpdatedAt
            }).ToList()
        };

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        return View(await BuildCreateViewModelAsync(cancellationToken));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TicketCreateViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            await PopulateCreateSelectListsAsync(model, cancellationToken);
            return View(model);
        }

        if (!TicketEnumParser.TryParsePriority(model.Priority, out var priority))
        {
            ModelState.AddModelError(nameof(model.Priority), "Invalid priority value.");
            await PopulateCreateSelectListsAsync(model, cancellationToken);
            return View(model);
        }

        var result = await _ticketService.CreateAsync(
            new CreateTicketRequest
            {
                Title = model.Title,
                Description = model.Description,
                Priority = priority,
                AssignedToId = model.AssignedToId!.Value,
                CreatedById = model.CreatedById!.Value
            },
            cancellationToken);

        if (!result.IsSuccess)
        {
            result.AddErrorsToModelState(ModelState);
            await PopulateCreateSelectListsAsync(model, cancellationToken);
            return View(model);
        }

        TempData["SuccessMessage"] = "Ticket created successfully.";
        return RedirectToAction(nameof(Details), new { id = result.Value!.Id });
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var viewModel = await BuildDetailsViewModelAsync(id, cancellationToken);
        if (viewModel is null)
        {
            return TicketNotFound();
        }

        viewModel.SuccessMessage = TempData["SuccessMessage"] as string;
        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var result = await _ticketService.GetByIdAsync(id, cancellationToken);
        if (!result.IsSuccess)
        {
            return TicketNotFound();
        }

        var ticket = result.Value!;
        var model = new TicketEditViewModel
        {
            Id = ticket.Id,
            Title = ticket.Title,
            Description = ticket.Description,
            Priority = ticket.Priority,
            Status = ticket.Status,
            AssignedToId = ticket.AssignedTo.Id
        };

        await PopulateEditSelectListsAsync(model, cancellationToken);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, TicketEditViewModel model, CancellationToken cancellationToken)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            await PopulateEditSelectListsAsync(model, cancellationToken);
            return View(model);
        }

        if (!TicketEnumParser.TryParsePriority(model.Priority, out var priority))
        {
            ModelState.AddModelError(nameof(model.Priority), "Invalid priority value.");
            await PopulateEditSelectListsAsync(model, cancellationToken);
            return View(model);
        }

        var result = await _ticketService.UpdateAsync(
            id,
            new UpdateTicketRequest
            {
                Title = model.Title,
                Description = model.Description,
                Priority = priority,
                AssignedToId = model.AssignedToId!.Value
            },
            cancellationToken);

        if (!result.IsSuccess)
        {
            result.AddErrorsToModelState(ModelState);
            await PopulateEditSelectListsAsync(model, cancellationToken);
            return View(model);
        }

        TempData["SuccessMessage"] = "Ticket updated successfully.";
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeStatus(
        int id,
        [Bind(Prefix = "StatusForm")] ChangeStatusViewModel statusForm,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(statusForm.NewStatus))
        {
            ModelState.AddModelError("StatusForm.NewStatus", "Please select a status.");
        }

        if (!ModelState.IsValid)
        {
            var invalidViewModel = await BuildDetailsViewModelAsync(id, cancellationToken);
            if (invalidViewModel is null)
            {
                return TicketNotFound();
            }

            invalidViewModel.StatusForm.NewStatus = statusForm.NewStatus;
            return View("Details", invalidViewModel);
        }

        if (!TicketEnumParser.TryParseStatus(statusForm.NewStatus, out var newStatus))
        {
            var viewModel = await BuildDetailsViewModelAsync(id, cancellationToken);
            if (viewModel is null)
            {
                return TicketNotFound();
            }

            viewModel.ErrorMessage = "Invalid status value.";
            return View("Details", viewModel);
        }

        var result = await _ticketService.ChangeStatusAsync(
            id,
            new ChangeTicketStatusRequest { Status = newStatus },
            cancellationToken);

        if (!result.IsSuccess)
        {
            var viewModel = await BuildDetailsViewModelAsync(id, cancellationToken);
            if (viewModel is null)
            {
                return TicketNotFound();
            }

            viewModel.ErrorMessage = result.ErrorMessage;
            return View("Details", viewModel);
        }

        TempData["SuccessMessage"] = "Status updated successfully.";
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddComment(
        int id,
        [Bind(Prefix = "CommentForm")] AddCommentViewModel commentForm,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var viewModel = await BuildDetailsViewModelAsync(id, cancellationToken, commentForm);
            if (viewModel is null)
            {
                return TicketNotFound();
            }

            return View("Details", viewModel);
        }

        var result = await _commentService.AddAsync(
            id,
            new CreateCommentRequest
            {
                Message = commentForm.Message,
                CreatedById = commentForm.CreatedById!.Value
            },
            cancellationToken);

        if (!result.IsSuccess)
        {
            var viewModel = await BuildDetailsViewModelAsync(id, cancellationToken, commentForm);
            if (viewModel is null)
            {
                return TicketNotFound();
            }

            result.AddErrorsToModelState(ModelState);
            viewModel.CommentForm = commentForm;
            await PopulateCommentSelectListsAsync(viewModel.CommentForm, cancellationToken);
            return View("Details", viewModel);
        }

        TempData["SuccessMessage"] = "Comment added successfully.";
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpGet]
    public IActionResult Error()
    {
        return View();
    }

    private IActionResult TicketNotFound()
    {
        Response.StatusCode = StatusCodes.Status404NotFound;
        return View("NotFound");
    }

    private async Task<TicketDetailsViewModel?> BuildDetailsViewModelAsync(
        int id,
        CancellationToken cancellationToken,
        AddCommentViewModel? commentForm = null)
    {
        var result = await _ticketService.GetByIdAsync(id, cancellationToken);
        if (!result.IsSuccess)
        {
            return null;
        }

        var ticket = result.Value!;
        var allowedTransitions = StatusTransitionHelper.GetAllowedTransitionNames(ticket.Status);

        var statusForm = new ChangeStatusViewModel
        {
            IsTerminal = StatusTransitionHelper.IsTerminalStatus(ticket.Status),
            AllowedStatusOptions = allowedTransitions
                .Select(value => new SelectListItem(TicketDisplayHelper.FormatStatus(value), value))
                .ToList()
        };

        var comment = commentForm ?? new AddCommentViewModel();
        await PopulateCommentSelectListsAsync(comment, cancellationToken);

        return new TicketDetailsViewModel
        {
            Ticket = new TicketDetailViewModel
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Priority = ticket.Priority,
                Status = ticket.Status,
                AssigneeName = ticket.AssignedTo.Name,
                CreatorName = ticket.CreatedBy.Name,
                CreatedAt = ticket.CreatedAt,
                UpdatedAt = ticket.UpdatedAt,
                Comments = ticket.Comments.Select(item => new CommentItemViewModel
                {
                    Id = item.Id,
                    Message = item.Message,
                    AuthorName = item.CreatedBy.Name,
                    CreatedAt = item.CreatedAt
                }).ToList()
            },
            StatusForm = statusForm,
            CommentForm = comment
        };
    }

    private async Task<TicketCreateViewModel> BuildCreateViewModelAsync(CancellationToken cancellationToken)
    {
        var model = new TicketCreateViewModel();
        await PopulateCreateSelectListsAsync(model, cancellationToken);
        return model;
    }

    private async Task PopulateCreateSelectListsAsync(TicketCreateViewModel model, CancellationToken cancellationToken)
    {
        model.PriorityOptions = BuildPriorityOptions(model.Priority);
        model.UserOptions = await BuildUserOptionsAsync(model.CreatedById ?? model.AssignedToId, cancellationToken);
    }

    private async Task PopulateEditSelectListsAsync(TicketEditViewModel model, CancellationToken cancellationToken)
    {
        model.PriorityOptions = BuildPriorityOptions(model.Priority);
        model.UserOptions = await BuildUserOptionsAsync(model.AssignedToId, cancellationToken);
    }

    private async Task PopulateCommentSelectListsAsync(AddCommentViewModel model, CancellationToken cancellationToken)
    {
        model.UserOptions = await BuildUserOptionsAsync(model.CreatedById, cancellationToken);
    }

    private static IReadOnlyList<SelectListItem> BuildPriorityOptions(string? selected)
    {
        return
        [
            new SelectListItem("Low", "Low", selected == "Low"),
            new SelectListItem("Medium", "Medium", selected == "Medium"),
            new SelectListItem("High", "High", selected == "High")
        ];
    }

    private static IReadOnlyList<SelectListItem> BuildStatusFilterOptions(string? selected)
    {
        var options = new List<SelectListItem>
        {
            new("All", string.Empty, string.IsNullOrWhiteSpace(selected))
        };

        foreach (var status in new[] { "Open", "InProgress", "Resolved", "Closed", "Cancelled" })
        {
            options.Add(new SelectListItem(
                TicketDisplayHelper.FormatStatus(status),
                status,
                string.Equals(selected, status, StringComparison.OrdinalIgnoreCase)));
        }

        return options;
    }

    private async Task<IReadOnlyList<SelectListItem>> BuildUserOptionsAsync(int? selectedId, CancellationToken cancellationToken)
    {
        var users = await _userService.GetAllAsync(cancellationToken);
        return users
            .Select(user => new SelectListItem(user.Name, user.Id.ToString(), user.Id == selectedId))
            .ToList();
    }
}

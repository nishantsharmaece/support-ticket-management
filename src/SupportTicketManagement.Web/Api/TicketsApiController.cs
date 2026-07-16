using Microsoft.AspNetCore.Mvc;
using SupportTicketManagement.Application.Comments;
using SupportTicketManagement.Application.Common;
using SupportTicketManagement.Application.Interfaces;
using SupportTicketManagement.Application.Tickets;

namespace SupportTicketManagement.Web.Api;

[ApiController]
[Route("api/tickets")]
[Produces("application/json")]
public sealed class TicketsApiController : ControllerBase
{
    private readonly ITicketService _ticketService;
    private readonly ICommentService _commentService;

    public TicketsApiController(ITicketService ticketService, ICommentService commentService)
    {
        _ticketService = ticketService;
        _commentService = commentService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(TicketDetailDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create(
        [FromBody] CreateTicketRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _ticketService.CreateAsync(request, cancellationToken);
        return ApiResults.FromResult(result, ticket =>
            CreatedAtAction(nameof(GetById), new { id = ticket.Id }, ticket));
    }

    [HttpGet]
    [ProducesResponseType(typeof(TicketListResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> List(
        [FromQuery] string? search,
        [FromQuery] string? status,
        CancellationToken cancellationToken)
    {
        if (!TicketEnumParser.TryParseOptionalStatusFilter(status, out var statusFilter))
        {
            return ApiResults.BadRequest("Invalid status value.", "status");
        }

        var result = await _ticketService.ListAsync(
            new TicketListQuery { Search = search, Status = statusFilter },
            cancellationToken);

        return ApiResults.FromResult(result, Ok);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TicketDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _ticketService.GetByIdAsync(id, cancellationToken);
        return ApiResults.FromResult(result, Ok);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(TicketDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateTicketRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _ticketService.UpdateAsync(id, request, cancellationToken);
        return ApiResults.FromResult(result, Ok);
    }

    [HttpPatch("{id:int}/status")]
    [ProducesResponseType(typeof(TicketDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> ChangeStatus(
        int id,
        [FromBody] ChangeTicketStatusRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _ticketService.ChangeStatusAsync(id, request, cancellationToken);
        return ApiResults.FromResult(result, Ok);
    }

    [HttpPost("{id:int}/comments")]
    [ProducesResponseType(typeof(CommentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddComment(
        int id,
        [FromBody] CreateCommentRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _commentService.AddAsync(id, request, cancellationToken);
        return ApiResults.FromResult(result, comment =>
            Created($"/api/tickets/{id}/comments/{comment.Id}", comment));
    }
}

using SupportTicketManagement.Domain.Enums;

namespace SupportTicketManagement.Domain.Entities;

public class Ticket
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Priority Priority { get; set; }

    public TicketStatus Status { get; set; } = TicketStatus.Open;

    public int AssignedToId { get; set; }

    public int CreatedById { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public User AssignedTo { get; set; } = null!;

    public User CreatedBy { get; set; } = null!;

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}

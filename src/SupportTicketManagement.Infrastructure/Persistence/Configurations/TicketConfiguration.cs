using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupportTicketManagement.Domain.Entities;
using SupportTicketManagement.Domain.Enums;

namespace SupportTicketManagement.Infrastructure.Persistence.Configurations;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("Tickets");

        builder.HasKey(ticket => ticket.Id);

        builder.Property(ticket => ticket.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(ticket => ticket.Description)
            .HasMaxLength(4000);

        builder.Property(ticket => ticket.Priority)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(ticket => ticket.Status)
            .IsRequired()
            .HasConversion<int>()
            .HasDefaultValue(TicketStatus.Open);

        builder.Property(ticket => ticket.CreatedAt)
            .IsRequired();

        builder.Property(ticket => ticket.UpdatedAt)
            .IsRequired();

        builder.HasIndex(ticket => ticket.Status)
            .HasDatabaseName("IX_Ticket_Status");

        builder.HasMany(ticket => ticket.Comments)
            .WithOne(comment => comment.Ticket)
            .HasForeignKey(comment => comment.TicketId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

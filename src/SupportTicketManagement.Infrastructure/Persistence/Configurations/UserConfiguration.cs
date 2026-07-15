using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupportTicketManagement.Domain.Entities;

namespace SupportTicketManagement.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(user => user.Email)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(user => user.Role)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(user => user.Email)
            .IsUnique()
            .HasDatabaseName("UQ_User_Email");

        builder.HasMany(user => user.CreatedTickets)
            .WithOne(ticket => ticket.CreatedBy)
            .HasForeignKey(ticket => ticket.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(user => user.AssignedTickets)
            .WithOne(ticket => ticket.AssignedTo)
            .HasForeignKey(ticket => ticket.AssignedToId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(user => user.Comments)
            .WithOne(comment => comment.CreatedBy)
            .HasForeignKey(comment => comment.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

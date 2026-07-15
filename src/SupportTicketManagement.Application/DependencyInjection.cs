using Microsoft.Extensions.DependencyInjection;
using SupportTicketManagement.Application.Comments;
using SupportTicketManagement.Application.Interfaces;
using SupportTicketManagement.Application.Tickets;
using SupportTicketManagement.Application.Users;

namespace SupportTicketManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ITicketService, TicketService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SupportTicketManagement.Infrastructure.Identity;

namespace SupportTicketManagement.Infrastructure.Seeding;

public static class IdentitySeeder
{
    public const string AdminRoleName = "Admin";
    public const string AdminEmail = "admin@example.com";
    public const string AdminPassword = "Admin@123";

    public static async Task SeedAsync(IServiceProvider services, CancellationToken cancellationToken = default)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        if (!await roleManager.RoleExistsAsync(AdminRoleName))
        {
            await roleManager.CreateAsync(new IdentityRole(AdminRoleName));
        }

        var adminUser = await userManager.FindByEmailAsync(AdminEmail);
        if (adminUser is not null)
        {
            return;
        }

        adminUser = new ApplicationUser
        {
            UserName = AdminEmail,
            Email = AdminEmail,
            EmailConfirmed = true
        };

        var createResult = await userManager.CreateAsync(adminUser, AdminPassword);
        if (!createResult.Succeeded)
        {
            var errors = string.Join(", ", createResult.Errors.Select(error => error.Description));
            throw new InvalidOperationException($"Failed to seed admin user: {errors}");
        }

        await userManager.AddToRoleAsync(adminUser, AdminRoleName);
    }
}

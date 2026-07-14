using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SupportTicketManagement.Infrastructure.Persistence;
using SupportTicketManagement.Infrastructure.Seeding;

namespace SupportTicketManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        var connectionString = ResolveSqliteConnectionString(configuration);

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString));

        return services;
    }

    public static async Task ApplyMigrationsAndSeedAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await context.Database.MigrateAsync();
        await DatabaseSeeder.SeedAsync(context);
    }

    private static string ResolveSqliteConnectionString(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");

        const string dataSourcePrefix = "Data Source=";
        if (!connectionString.StartsWith(dataSourcePrefix, StringComparison.OrdinalIgnoreCase))
        {
            return connectionString;
        }

        var dataSource = connectionString[dataSourcePrefix.Length..].Trim();
        if (Path.IsPathRooted(dataSource))
        {
            var directory = Path.GetDirectoryName(dataSource);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return connectionString;
        }

        var resolvedPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), dataSource));
        var resolvedDirectory = Path.GetDirectoryName(resolvedPath);
        if (!string.IsNullOrEmpty(resolvedDirectory))
        {
            Directory.CreateDirectory(resolvedDirectory);
        }

        return $"{dataSourcePrefix}{resolvedPath}";
    }
}

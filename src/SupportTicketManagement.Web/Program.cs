using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using SupportTicketManagement.Application;
using SupportTicketManagement.Application.Common;
using SupportTicketManagement.Infrastructure;
using SupportTicketManagement.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(entry => entry.Value?.Errors.Count > 0)
            .SelectMany(entry => entry.Value!.Errors.Select(error => new ValidationError(
                ToCamelCaseField(entry.Key),
                string.IsNullOrWhiteSpace(error.ErrorMessage) ? "Invalid value." : error.ErrorMessage)))
            .ToList();

        return new BadRequestObjectResult(new ErrorResponseDto
        {
            Title = "Validation failed",
            Status = StatusCodes.Status400BadRequest,
            Errors = errors
        });
    };
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

await app.Services.ApplyMigrationsAndSeedAsync();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapControllers();

app.Run();

static string ToCamelCaseField(string fieldName)
{
    if (string.IsNullOrWhiteSpace(fieldName))
    {
        return string.Empty;
    }

    var segments = fieldName.Split('.', StringSplitOptions.RemoveEmptyEntries);
    return segments[^1];
}

public partial class Program;

using System.Text.Json;
using SupportTicketManagement.Application.Common;

namespace SupportTicketManagement.Web.Middleware;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unhandled exception processing {Method} {Path}", context.Request.Method, context.Request.Path);

            if (context.Response.HasStarted)
            {
                throw;
            }

            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            if (context.Request.Path.StartsWithSegments("/api"))
            {
                context.Response.ContentType = "application/json";

                var response = new ErrorResponseDto
                {
                    Title = "An error occurred",
                    Status = StatusCodes.Status500InternalServerError,
                    Errors =
                    [
                        new ValidationError(string.Empty, "An unexpected error occurred.")
                    ]
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response, JsonSerializerOptions));
                return;
            }

            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.WriteAsync(
                "<html><body><h1>Something went wrong</h1><p>An unexpected error occurred. Please try again.</p>" +
                "<a href=\"/Tickets\">Back to ticket list</a></body></html>");
        }
    }

    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
}

using Microsoft.AspNetCore.Mvc;
using SupportTicketManagement.Application.Common;

namespace SupportTicketManagement.Web.Api;

public static class ApiResults
{
    public static IActionResult FromResult<T>(ServiceResult<T> result, Func<T, IActionResult> onSuccess)
    {
        if (result.IsSuccess)
        {
            return onSuccess(result.Value!);
        }

        return result.ErrorKind switch
        {
            ServiceErrorKind.Validation => BadRequest(result),
            ServiceErrorKind.NotFound => NotFound(result),
            ServiceErrorKind.Conflict => Conflict(result),
            _ => ServerError()
        };
    }

    public static IActionResult BadRequest(string message, string field = "")
    {
        return new BadRequestObjectResult(CreateErrorResponse(400, "Validation failed", message, field));
    }

    private static BadRequestObjectResult BadRequest<T>(ServiceResult<T> result)
    {
        return new BadRequestObjectResult(CreateErrorResponse(400, "Validation failed", result));
    }

    private static NotFoundObjectResult NotFound<T>(ServiceResult<T> result)
    {
        return new NotFoundObjectResult(CreateErrorResponse(404, "Not found", result));
    }

    private static ObjectResult Conflict<T>(ServiceResult<T> result)
    {
        return new ObjectResult(CreateErrorResponse(
            409,
            "Conflict",
            result.ErrorMessage ?? "Conflict.",
            "status"))
        {
            StatusCode = StatusCodes.Status409Conflict
        };
    }

    private static ObjectResult ServerError()
    {
        return new ObjectResult(new ErrorResponseDto
        {
            Title = "An error occurred",
            Status = StatusCodes.Status500InternalServerError,
            Errors =
            [
                new ValidationError(string.Empty, "An unexpected error occurred.")
            ]
        })
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
    }

    private static ErrorResponseDto CreateErrorResponse<T>(int status, string title, ServiceResult<T> result)
    {
        if (result.ValidationErrors.Count > 0)
        {
            return new ErrorResponseDto
            {
                Title = title,
                Status = status,
                Errors = result.ValidationErrors
            };
        }

        return CreateErrorResponse(status, title, result.ErrorMessage ?? "Request failed.", string.Empty);
    }

    private static ErrorResponseDto CreateErrorResponse(int status, string title, string message, string field)
    {
        return new ErrorResponseDto
        {
            Title = title,
            Status = status,
            Errors = [new ValidationError(field, message)]
        };
    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding;
using SupportTicketManagement.Application.Common;

namespace SupportTicketManagement.Web.Helpers;

public static class ServiceResultMvcExtensions
{
    public static void AddErrorsToModelState<T>(this ServiceResult<T> result, ModelStateDictionary modelState)
    {
        if (result.IsSuccess)
        {
            return;
        }

        if (result.ValidationErrors.Count > 0)
        {
            foreach (var error in result.ValidationErrors)
            {
                var field = string.IsNullOrWhiteSpace(error.Field) ? string.Empty : error.Field;
                modelState.AddModelError(field, error.Message);
            }

            return;
        }

        modelState.AddModelError(string.Empty, result.ErrorMessage ?? "The request could not be completed.");
    }
}

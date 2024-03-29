using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RestaurantReservation.API.Contracts.Responses;

namespace RestaurantReservation.API.Filters;

public class ModelValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(modelState => modelState.Value?.Errors.Count > 0)
                .ToDictionary(kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(error => error.ErrorMessage).ToArray() ?? Array.Empty<string>());
            var errorResponse = new ErrorResponse
            {
                RequestPath = context.HttpContext.Request.Path,
                Errors = errors
            };
            context.Result = new BadRequestObjectResult(errorResponse);
            return;
        }
        await next();
    }
}
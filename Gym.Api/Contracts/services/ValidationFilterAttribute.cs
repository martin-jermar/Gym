namespace Gym.Api.Contracts.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public class ValidationFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());

            var response = new ValidationProblemDetails
            {
                Title = "Validation Failed",
                
                Status = StatusCodes.Status403Forbidden,
                Errors = errors,
            };

            context.Result = new BadRequestObjectResult(response);
        }
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
    }
}
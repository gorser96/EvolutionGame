using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EvolutionBack.Core;

public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
{
    public int Order => int.MaxValue - 10;

    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is { } ex)
        {
            context.Result = new ObjectResult(ex.Message)
            {
                StatusCode = 400,
            };
        };

        context.ExceptionHandled = true;
    }
}

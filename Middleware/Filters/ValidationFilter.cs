using Microsoft.AspNetCore.Mvc.Filters;
using Restaurant.Core.Application.Exceptions;
using System.Net;

namespace Middleware.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var error = context.ModelState.SelectMany(x => x.Value.Errors).FirstOrDefault();
                throw new RestaurantException(error.ErrorMessage, HttpStatusCode.BadRequest);
            }
            await next();
        }
    }
}

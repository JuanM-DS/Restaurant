using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Restaurant.Core.Application.CustomEntities;
using Restaurant.Core.Application.Exceptions;

namespace Middleware.Filters
{
    public class GlobalException : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if(filterContext.Exception.GetType() == typeof(Restaurant.Core.Application.Exceptions.RestaurantException))
            {
                var exception = filterContext.Exception as Restaurant.Core.Application.Exceptions.RestaurantException;

                var response = new Response<object>()
                {
                    Error = exception.Message,
                    Success = false,
                    StatusCode = exception.StatusCode
                };

                var json = new
                {
                    errors = new[] { response }
                };

                filterContext.Result = new ObjectResult(json) { StatusCode = (int)exception.StatusCode };
                filterContext.HttpContext.Response.StatusCode = (int)response.StatusCode;
                filterContext.ExceptionHandled = true;
            }
        }
    }
}

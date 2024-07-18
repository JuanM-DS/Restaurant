using System.Net;

namespace Restaurant.Core.Application.Exceptions
{
    public class RestaurantException(string message, HttpStatusCode statusCode) : Exception(message)
    {
        public HttpStatusCode StatusCode { get; } = statusCode;
    }
}

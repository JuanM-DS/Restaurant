using Microsoft.AspNetCore.WebUtilities;
using Restaurant.Core.Application.Interfaces.Persistence.Services;

namespace Restaurant.Infrastructure.Shared.Services
{
    public class UriServices : IUriServices
    {
        private readonly string _origin;

        public UriServices(string origin)
        {
            _origin = origin;
        }

        public string GetResetPasswordURl(string token, string userId)
        {
            var route = "/User/ResetPassword";

            var uri = new Uri(string.Concat(_origin, route));
            var finalUrl = QueryHelpers.AddQueryString(uri.ToString(), "Token", token);
            finalUrl = QueryHelpers.AddQueryString(finalUrl, "UserId", userId);

            return finalUrl;
        }
    }
}

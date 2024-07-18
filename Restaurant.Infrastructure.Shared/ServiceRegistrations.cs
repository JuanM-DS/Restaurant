using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Core.Application.Interfaces.Services;
using Restaurant.Core.Domain.Settings;
using Restaurant.Infrastructure.Shared.Services;

namespace Restaurant.Infrastructure.Shared
{
    public static class ServiceRegistrations
    {
        public static void AddSharedLayer(this IServiceCollection service, IConfiguration configuration)
        {
            #region settings
            service.Configure<EmailSettings>(x => configuration.GetSection("EmailSettings").Bind(x));
            #endregion

            #region services
            service.AddTransient<IEmailService, EmailServices>();
            service.AddSingleton<IUriServices>(provider =>
            {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var origin = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriServices(origin);
            });
            #endregion
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Middleware.Filters;
using Restaurant.Core.Application.Interfaces.Services;
using Restaurant.Core.Domain.Settings;
using Restaurant.Infrastructure.Shared.Services;
using FluentValidation.AspNetCore;

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

            #region fluent validations
            service.AddMvc(option => option.Filters.Add<ValidationFilter>())
                .AddFluentValidation(provider => provider.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            #endregion
        }
    }
}

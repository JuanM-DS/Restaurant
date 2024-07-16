using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Core.Application.Interfaces.Shared.Services;
using Restaurant.Core.Domain.Settings;
using Restaurant.Infrastructure.Shared.Services;

namespace Restaurant.Infrastructure.Shared
{
    public static class ServiceRegistrations
    {
        public static void AddSharedLayer(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<EmailSettings>(x => configuration.GetSection("EmailSettings").Bind(x));

            service.AddTransient<IEmailService, EmailService>();
        }
    }
}

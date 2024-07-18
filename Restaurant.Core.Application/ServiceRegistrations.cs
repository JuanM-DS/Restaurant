using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Core.Domain.Settings;

namespace Restaurant.Core.Application
{
    public static class ServiceRegistrations
    {
        public static void AddApplicatinLayer(this IServiceCollection service, IConfiguration configuration)
        {
            #region settings
            service.Configure<PaginationSettings>(x => configuration.GetSection("PaginationSettings").Bind(x));
            #endregion
        }
    }
}

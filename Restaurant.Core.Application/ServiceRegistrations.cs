using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Core.Application.Interfaces.Services;
using Restaurant.Core.Application.Services;
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

            #region services
            service.AddTransient<IDishServices, DishServices>();
            service.AddTransient<IDishCategoryServices, DishCategoryServices>();
            service.AddTransient<IIngredientServices, IngredientServices>();
            service.AddTransient<IOrderServices, OrderServices>();
            service.AddTransient<IOrderStatusServices, OrderStatusServices>();
            service.AddTransient<ITableServices, TableServices>();
            service.AddTransient<ITableStatusServices, TableStatusServices>();
            service.AddTransient<IUserServices, UserServices>();
            #endregion
        }
    }
}

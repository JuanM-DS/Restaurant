using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Infrastructure.Persistence.Context;
using Restaurant.Infrastructure.Persistence.Repositories;

namespace Restaurant.Infrastructure.Persistence
{
    public static class ServiceRegistrations
    {
        public static void AddPersistenceLayer(this IServiceCollection service, IConfiguration configuration)
        {
            #region context
            var sqlConnection = configuration.GetConnectionString("sqlConnection")
                                        ?? throw new InvalidOperationException("The connection string is missing from the configuration.");

            service.AddDbContext<RestaurantDbContext>(provider =>
                provider.UseSqlServer(configuration.GetConnectionString("sqlConnection"), m => m.MigrationsAssembly(typeof(RestaurantDbContext).Assembly.FullName))
            );
            #endregion

            #region Repositories
            service.AddTransient<IDishRepository, DishRepository>();
            service.AddTransient<IDishCategoryRepository, DishCategoryRepository>();
            service.AddTransient<IIngredientRepository, IngredientRepository>();
            service.AddTransient<IOrderRepository, OrderRepository>();
            service.AddTransient<IOrderStatusRepository, OrderStatusRepository>();
            service.AddTransient<ITableRepository, TableRepository>();
            service.AddTransient<ITableStatusRepository, TableStatusRepository>();
            #endregion
        }
    }
}

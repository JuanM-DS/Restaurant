using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Infrastructure.Identity.Context;
using Restaurant.Infrastructure.Identity.Entities;
using Restaurant.Infrastructure.Identity.Seeds;

namespace Restaurant.Infrastructure.Identity
{
    public static class ServiceRegistration
    {
        public static void AddServiceIdentityLayer(this IServiceCollection service, IConfiguration configuration)
        {
            #region context
            var sqlIdentityConnection = configuration.GetConnectionString("sqlIdentityConnection")
                                        ?? throw new InvalidOperationException("The connection string is missing from the configuration.");

            service.AddDbContext<RestaurantIdentityDbContext>(provider =>
                provider.UseSqlServer(sqlIdentityConnection, m => m.MigrationsAssembly(typeof(RestaurantIdentityDbContext).Assembly.FullName))
            );
            #endregion

            #region identity
            service.AddIdentity<ApplicationUser, ApplicationRole>()
                    .AddEntityFrameworkStores<RestaurantIdentityDbContext>()
                    .AddDefaultTokenProviders();
            #endregion
        }

        public static async Task RunSeedsAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var service  = scope.ServiceProvider;

            var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = service.GetRequiredService<RoleManager<ApplicationRole>>();

            await DefaultAdmin.CreateSeed(userManager);
            await DefaultSuperAdmin.CreateSeed(userManager);
            await DefaultRoles.CreateSeed(roleManager);
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Restaurant.Core.Application.Enums;
using Restaurant.Infrastructure.Identity.Entities;

namespace Restaurant.Infrastructure.Identity.Seeds
{
    public class DefaultRoles
    {
        public static async Task CreateSeed(RoleManager<ApplicationRole> roleManager)
        {
            var waiterRole = new ApplicationRole()
            {
                Name = RoleTypes.Waiter.ToString(),
                NormalizedName = "WAITER",
                CreatedBy = "System",
                CreatedTime = DateTime.Now
            };

            var adminRole = new ApplicationRole()
            {
                Name = RoleTypes.Admin.ToString(),
                NormalizedName = "ADMIN",
                CreatedBy = "System",
                CreatedTime = DateTime.Now
            };

            var waiterExists = await roleManager.RoleExistsAsync(waiterRole.Name);
            var adminExists = await roleManager.RoleExistsAsync(adminRole.Name);

            if (!waiterExists)
                await roleManager.CreateAsync(waiterRole);

            if (!adminExists)
                await roleManager.CreateAsync(adminRole);
        }
    }
}

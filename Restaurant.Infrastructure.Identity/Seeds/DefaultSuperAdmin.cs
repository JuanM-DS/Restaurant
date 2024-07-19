using Microsoft.AspNetCore.Identity;
using Restaurant.Core.Application.Enums;
using Restaurant.Infrastructure.Identity.Entities;

namespace Restaurant.Infrastructure.Identity.Seeds
{
    public static class DefaultSuperAdmin
    {
        public static async Task CreateSeed(UserManager<ApplicationUser> userManager)
        {
            var superAdmin = new ApplicationUser()
            {
                UserName = "SuperAdmin",
                FirstName = "Jane",
                LastName = "Doe",
                Email = "superAdmin@email.com",
                EmailConfirmed = true,
                CreatedBy = "System",
                CreatedTime = DateTime.Now,
                NormalizedEmail = "SUPERADMIN@EMAIL.COM",
                NormalizedUserName = "SUPERADMIN"
            };

            var superAdminByName = await userManager.FindByNameAsync(superAdmin.UserName);
            if (superAdminByName is not null)
                return;

            var superAdminByEmail = await userManager.FindByEmailAsync(superAdmin.Email);
            if (superAdminByEmail is not null)
                return;

            var result = await userManager.CreateAsync(superAdmin, "123Pa$$word!");
            if (!result.Succeeded)
                return;

            await userManager.AddToRolesAsync(superAdmin, [RoleTypes.Waiter.ToString(), RoleTypes.Admin.ToString()]);
        }
    }
}

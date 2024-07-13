using Microsoft.AspNetCore.Identity;
using Restaurant.Core.Application.Enums;
using Restaurant.Infrastructure.Identity.Entities;

namespace Restaurant.Infrastructure.Identity.Seeds
{
    public static class DefaultAdmin
    {
        public static async Task CreateSeed(UserManager<ApplicationUser> userManager)
        {
            var admin = new ApplicationUser() 
            { 
                UserName = "Admin",
                FirstName = "Jhone",
                LastName = "Doe",
                Email = "waiter@email.com",
                EmailConfirmed = true,
                CreatedBy = "System",
                CreatedTime = DateTime.Now,
                NormalizedEmail = "ADMIN@EMAIL.COM",
                NormalizedUserName = "ADMIN"
            };

            var adminByName = await userManager.FindByNameAsync(admin.UserName);
            if (adminByName is not null)
                return;

            var adminByEmail = await userManager.FindByEmailAsync(admin.Email);
            if (adminByEmail is not null)
                return;

            var result = await userManager.CreateAsync(admin);
            if (!result.Succeeded)
                return;

            await userManager.AddToRoleAsync(admin, RoleTypes.Admin.ToString());
        }
    }
}

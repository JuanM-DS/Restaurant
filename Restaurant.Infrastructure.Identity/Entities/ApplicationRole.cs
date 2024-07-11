using Microsoft.AspNetCore.Identity;

namespace Restaurant.Infrastructure.Identity.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUser> Users { get; set; } = [];
    }
}

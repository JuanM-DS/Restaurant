using Microsoft.AspNetCore.Identity;

namespace Restaurant.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser, IAuditableBaseEntity
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedTime { get; set; }

        public string? LastModifiedBy { get; set; }

        public DateTime? LastModifiedTime { get; set; }

        //Navigators
        public ICollection<ApplicationRole> Roles { get; set; } = [];
    }
}

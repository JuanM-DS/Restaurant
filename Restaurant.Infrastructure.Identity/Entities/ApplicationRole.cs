using Microsoft.AspNetCore.Identity;
using Restaurant.Core.Application.Interfaces.Shared.CommonEntities;

namespace Restaurant.Infrastructure.Identity.Entities
{
    public class ApplicationRole : IdentityRole, IAuditableBaseEntity
    {
        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedTime { get ; set; }

        public string? LastModifiedBy { get ; set ; }

        public DateTime? LastModifiedTime { get ; set ; }

        //Navigators
        public ICollection<ApplicationUser> Users { get; set; } = [];
    }
}

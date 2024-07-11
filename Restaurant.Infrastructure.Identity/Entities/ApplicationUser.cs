﻿using Microsoft.AspNetCore.Identity;

namespace Restaurant.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public ICollection<ApplicationRole> Roles { get; set; } = [];
    }
}

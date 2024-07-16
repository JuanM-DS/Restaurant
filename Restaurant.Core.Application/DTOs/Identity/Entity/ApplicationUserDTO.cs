﻿namespace Restaurant.Core.Application.DTOs.Identity.Entity
{
    public class ApplicationUserDTO
    {
        public string Id { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool EmailConfirmed { get; set; } 

        public DateTime CreatedTime { get; set; }

        //Navigators
        public ICollection<ApplicationRoleDTO> Roles { get; set; } = [];
    }
}
namespace Restaurant.Core.Application.DTOs.Entities
{
    public class ApplicationUserDto
    {
        public string Id { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool EmailConfirmed { get; set; }

        public DateTime CreatedTime { get; set; }

        //Navigators
        public ICollection<ApplicationRoleDto> Roles { get; set; } = [];
    }
}

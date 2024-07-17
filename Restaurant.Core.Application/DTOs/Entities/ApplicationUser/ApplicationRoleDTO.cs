namespace Restaurant.Core.Application.DTOs.Entities.ApplicationUser
{
    public class ApplicationRoleDto
    {
        public ApplicationRoleDto(string name)
        {
            Name = name;
        }

        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public DateTime CreatedTime { get; set; }

        //Navigators
        public ICollection<ApplicationUserDto> Users { get; set; } = [];
    }
}

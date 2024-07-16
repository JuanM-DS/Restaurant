namespace Restaurant.Core.Application.DTOs.Identity.Entity
{
    public class ApplicationRoleDTO
    {

        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public DateTime CreatedTime { get; set; }

        //Navigators
        public ICollection<ApplicationUserDTO> Users { get; set; } = [];
    }
}

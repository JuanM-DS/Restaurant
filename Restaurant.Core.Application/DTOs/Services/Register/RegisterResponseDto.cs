using Restaurant.Core.Application.DTOs.Entities.ApplicationUser;

namespace Restaurant.Core.Application.DTOs.Services.Register
{
    public class RegisterResponseDto
    {
        public ApplicationUserDto userDto { get; set; } = null!;

        public bool Success { get; set; }

        public string Error { get; set; } = null!;
    }
}

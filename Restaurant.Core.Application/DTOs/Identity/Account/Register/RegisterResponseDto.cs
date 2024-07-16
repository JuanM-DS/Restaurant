using Restaurant.Core.Application.DTOs.Identity.Entity;

namespace Restaurant.Core.Application.DTOs.Identity.Account.Register
{
    public class RegisterResponseDto
    {
        public ApplicationUserDto userDto { get; set; } = null!;

        public bool Success { get; set; }

        public string Error { get; set; } = null!;
    }
}

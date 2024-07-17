using Restaurant.Core.Application.DTOs.Entities.ApplicationUser;

namespace Restaurant.Core.Application.DTOs.Services.Authentitcation
{
    public class AuthenticationResponseDto
    {
        public ApplicationUserDto User { get; set; } = null!;

        public bool Success { get; set; }

        public string Error { get; set; } = null!;
    }
}

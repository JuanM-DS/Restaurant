using Restaurant.Core.Application.DTOs.Identity.Entity;

namespace Restaurant.Core.Application.DTOs.Identity.Account.Authentitcation
{
    public class AuthenticationResponseDto
    {
        public ApplicationUserDTO user { get; set; } = null!;

        public bool Success { get; set; }

        public string Error { get; set; } = null!;
    }
}

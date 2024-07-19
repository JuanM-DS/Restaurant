using Restaurant.Core.Application.DTOs.Entities;
using System.Text.Json.Serialization;

namespace Restaurant.Core.Application.DTOs.Services.Authentitcation
{
    public class AuthenticationResponseDto
    {
        public ApplicationUserDto User { get; set; } = null!;

        public bool Success { get; set; }

        public string Error { get; set; } = null!;

        public string JWToken { get; set; } = null!;

        [JsonIgnore]
        public string RefreshToken { get; set; } = null!;
    }
}

namespace Restaurant.Core.Application.DTOs.Identity.Account.Register
{
    public class RegisterResponseDto
    {
        public bool Success { get; set; }

        public string Error { get; set; } = null!;
    }
}

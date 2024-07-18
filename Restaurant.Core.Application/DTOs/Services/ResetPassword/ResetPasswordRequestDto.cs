namespace Restaurant.Core.Application.DTOs.Services.ResetPassword
{
    public class ResetPasswordRequestDto
    {
        public string Token { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string ConfirmPassword { get; set; } = null!;
    }
}

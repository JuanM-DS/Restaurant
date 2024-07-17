namespace Restaurant.Core.Application.DTOs.Services.ResetPassword
{
    public class ResetPasswordResponseDto
    {
        public bool Success { get; set; }

        public string Error { get; set; } = null!;
    }
}

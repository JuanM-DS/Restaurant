namespace Restaurant.Core.Application.DTOs.Services.ForgotPassword
{
    public class ForgotPasswordResponseDto
    {
        public bool Success { get; set; }

        public string Error { get; set; } = null!;
    }
}

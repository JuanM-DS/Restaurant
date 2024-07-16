namespace Restaurant.Core.Application.DTOs.Identity.Account.ForgotPassword
{
    public class ForgotPasswordResponseDto
    {
        public bool Success { get; set; }

        public string Error { get; set; } = null!;
    }
}

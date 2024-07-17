using Restaurant.Core.Application.DTOs.Entities.ApplicationUser;
using Restaurant.Core.Application.DTOs.Services.Authentitcation;
using Restaurant.Core.Application.DTOs.Services.ForgotPassword;
using Restaurant.Core.Application.DTOs.Services.Register;
using Restaurant.Core.Application.DTOs.Services.ResetPassword;

namespace Restaurant.Core.Application.Interfaces.Services
{
    public interface IAccountServices
    {
        public Task<AuthenticationResponseDto> AuthenticationAsync(AuthenticationRequestDto request);

        public Task<RegisterResponseDto> RegisterAsync(ApplicationUserDto user);

        public Task SignOutAsync();

        public Task<ForgotPasswordResponseDto> ForgotPasswordAsync(ForgotPasswordRequestDto request);

        public Task<ResetPasswordResponseDto> ResetPasswordAsync(ResetPasswordRequestDto request);
    }
}

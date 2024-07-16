using Restaurant.Core.Application.DTOs.Identity.Account.Authentitcation;
using Restaurant.Core.Application.DTOs.Identity.Account.ForgotPassword;
using Restaurant.Core.Application.DTOs.Identity.Account.Register;
using Restaurant.Core.Application.DTOs.Identity.Account.ResetPassword;
using Restaurant.Core.Application.DTOs.Identity.Entity;

namespace Restaurant.Core.Application.Interfaces.Persistence.Services
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

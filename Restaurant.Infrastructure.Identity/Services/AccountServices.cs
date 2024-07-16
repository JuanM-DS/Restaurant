using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Restaurant.Core.Application.DTOs.Identity.Account.Authentitcation;
using Restaurant.Core.Application.DTOs.Identity.Account.ForgotPassword;
using Restaurant.Core.Application.DTOs.Identity.Account.Register;
using Restaurant.Core.Application.DTOs.Identity.Account.ResetPassword;
using Restaurant.Core.Application.DTOs.Identity.Entity;
using Restaurant.Core.Application.DTOs.Shared.Email;
using Restaurant.Core.Application.Interfaces.Persistence.Services;
using Restaurant.Core.Application.Interfaces.Shared.Services;
using Restaurant.Infrastructure.Identity.Entities;
using System.Text;

namespace Restaurant.Infrastructure.Identity.Services
{
    public class AccountServices
        (
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IMapper mapper,
        IUriServices uriServices,
        IEmailService emailServices
        ) : IAccountServices
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly IMapper _mapper = mapper;
        private readonly IUriServices _uriServices = uriServices;
        private readonly IEmailService _emailServices = emailServices;

        public async Task<AuthenticationResponseDto> AuthenticationAsync(AuthenticationRequestDto request)
        {
            var userByName = await _userManager.FindByNameAsync(request.UserName);
            if (userByName is null)
                return new()
                {
                    Success = false,
                    Error = $"There is not any user with this userName: {request.UserName}"
                };

            var result = await _signInManager.PasswordSignInAsync(userByName, request.Password, true, lockoutOnFailure : false);
            if (!result.Succeeded)
                return new()
                {
                    Success = false,
                    Error = $"Your credentials are incorret"
                };

            var userDto = _mapper.Map<ApplicationUserDto>(userByName);

            var roles = await _userManager.GetRolesAsync(userByName).ConfigureAwait(false);
            userDto.Roles = roles.ToList().ConvertAll(x => new ApplicationRoleDto(x));

            return new()
            {
                User = userDto,
                Success = true,
            };
        }

        public async Task<ForgotPasswordResponseDto> ForgotPasswordAsync(ForgotPasswordRequestDto request)
        {
            var userByEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userByEmail is null)
                return new()
                {
                    Success = false,
                    Error = $"There is not any user with this email: {request.Email}"
                };

            var token = await _userManager.GeneratePasswordResetTokenAsync(userByEmail);
            var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var url = _uriServices.GetResetPasswordURl(code, userByEmail.Id);

            var email = new EmailRequest()
            {
                To = userByEmail.Email,
                Subject = "Reset Password",
                Body = $"Please reset your password here: {url}"
            };

            await _emailServices.SendAsycn(email);

            return new()
            {
                Success = true,
            };
        }

        public async Task<RegisterResponseDto> RegisterAsync(ApplicationUserDto request)
        {
            var userByName = await _userManager.FindByNameAsync(request.UserName);
            if (userByName is not  null)
                return new()
                {
                    Success = false,
                    Error = $"The userName: {request.UserName} is already taken"
                };

            var userByEmail = await _userManager.FindByNameAsync(request.Email);
            if (userByEmail is not null)
                return new()
                {
                    Success = false,
                    Error = $"The email: {request.Email} is already taken"
                };


            var user = _mapper.Map<ApplicationUser>(request);

            user.EmailConfirmed = true;

            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
                return new()
                {
                    Success = false,
                    Error = result.Errors.First().Description
                };

            var roles = request.Roles.Select(x => x.Name).ToList();
            await _userManager.AddToRolesAsync(user, roles);

            var userDto = _mapper.Map<ApplicationUserDto>(user);

            return new()
            {
                userDto = userDto,
                Success = true
            };
        }

        public async Task<ResetPasswordResponseDto> ResetPasswordAsync(ResetPasswordRequestDto request)
        {
            var userById = await _userManager.FindByIdAsync(request.UserId);
            if (userById is null)
                return new()
                {
                    Success = false,
                    Error = $"There is not any user with this id: {request.UserId}"
                };

            var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));

            var result = await _userManager.ResetPasswordAsync(userById, token, request.Password);
            if(!result.Succeeded)
                return new()
                {
                    Success = false,
                    Error = result.Errors.First().Description
                };

            return new()
            {
                Success = true,
            };
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}

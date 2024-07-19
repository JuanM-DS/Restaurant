using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Restaurant.Core.Application.DTOs.Entities;
using Restaurant.Core.Application.DTOs.Services.Authentitcation;
using Restaurant.Core.Application.DTOs.Services.Email;
using Restaurant.Core.Application.DTOs.Services.ForgotPassword;
using Restaurant.Core.Application.DTOs.Services.Register;
using Restaurant.Core.Application.DTOs.Services.ResetPassword;
using Restaurant.Core.Application.Interfaces.Services;
using Restaurant.Core.Domain.Settings;
using Restaurant.Infrastructure.Identity.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Restaurant.Infrastructure.Identity.Services
{
    public class AccountServices
        (
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IMapper mapper,
        IUriServices uriServices,
        IEmailService emailServices,
        IOptions<JwtSettings> jwtSettings
        ) : IAccountServices
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly IMapper _mapper = mapper;
        private readonly IUriServices _uriServices = uriServices;
        private readonly IEmailService _emailServices = emailServices;
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;

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

            var token = await GenerateJWtTokenAsync(userByName);
            
            return new()
            {
                User = userDto,
                Success = true,
                JWToken = token,
                RefreshToken = GenerateRefreshToken().Token
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
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return new()
                {
                    Success = false,
                    Error = result.Errors.First().Description
                };

            var roles = request.Roles.Select(x => x.Name).ToList();
            result = await _userManager.AddToRolesAsync(user, roles);
            if (!result.Succeeded)
                return new()
                {
                    Success = false,
                    Error = result.Errors.First().Description
                };

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

        private async Task<string> GenerateJWtTokenAsync(ApplicationUser user)
        {
            #region Header
            var symmetricSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSigningKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);
            #endregion

            #region Claims
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = roles.Select(x => new Claim("roles", x));

            var claims = new[]
            {
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Email, user.Email),
                new Claim("UserId", user.Id)
            }
            .Union(roleClaims)
            .Union(userClaims);
            #endregion

            #region Playload
            var payload = new JwtPayload(
                issuer : _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutenes)
                );
            #endregion

            #region final token
            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
            #endregion
        }

        public RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken()
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };
        }

        public string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var ramdonBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(ramdonBytes);

            return BitConverter.ToString(ramdonBytes).Replace("_", "");
        }
    }
}

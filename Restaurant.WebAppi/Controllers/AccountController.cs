using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.DTOs.Entities;
using Restaurant.Core.Application.DTOs.Services.Authentitcation;
using Restaurant.Core.Application.DTOs.Services.ForgotPassword;
using Restaurant.Core.Application.DTOs.Services.ResetPassword;
using Restaurant.Core.Application.Enums;
using Restaurant.Core.Application.Interfaces.Services;

namespace Restaurant.WebAppi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices _accountService;
        private readonly IMapper mapper;

        public AccountController(IAccountServices accountService, IMapper mapper)
        {
            _accountService = accountService;
            this.mapper = mapper;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequestDto request)
        {
            var result = await _accountService.AuthenticationAsync(request);
            return Ok(result);
        }

        [HttpPost("registerAdmin")]
        [Authorize(Roles = nameof(RoleTypes.Admin))]
        public async Task<IActionResult> RegisterAdminAsync(SaveApplicationUserDto request)
        {
            var dto = mapper.Map<ApplicationUserDto>(request);
            dto.Roles.Add(new ApplicationRoleDto("Admin"));

            return Ok(await _accountService.RegisterAsync(dto));
        }

        [HttpPost("registerWaiter")]
        public async Task<IActionResult> RegisterWaiterAsync(SaveApplicationUserDto request)
        {
            var dto = mapper.Map<ApplicationUserDto>(request);
            dto.Roles.Add(new ApplicationRoleDto("Waiter"));

            return Ok(await _accountService.RegisterAsync(dto));
        }
    }
}

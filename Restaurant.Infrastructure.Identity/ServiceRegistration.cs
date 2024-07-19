using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Restaurant.Core.Application.CustomEntities;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Application.Interfaces.Services;
using Restaurant.Core.Domain.Settings;
using Restaurant.Infrastructure.Identity.Context;
using Restaurant.Infrastructure.Identity.Entities;
using Restaurant.Infrastructure.Identity.Repositories;
using Restaurant.Infrastructure.Identity.Seeds;
using Restaurant.Infrastructure.Identity.Services;
using System.Text;

namespace Restaurant.Infrastructure.Identity
{
    public static class ServiceRegistration
    {
        public static void AddIdentityLayer(this IServiceCollection service, IConfiguration configuration)
        {
            #region context
            var sqlIdentityConnection = configuration.GetConnectionString("sqlIdentityConnection")
                                        ?? throw new InvalidOperationException("The connection string is missing from the configuration.");

            service.AddDbContext<RestaurantIdentityDbContext>(provider =>
                provider.UseSqlServer(sqlIdentityConnection, m => m.MigrationsAssembly(typeof(RestaurantIdentityDbContext).Assembly.FullName))
            );
            #endregion

            #region identity
            service.AddIdentity<ApplicationUser, ApplicationRole>()
                    .AddEntityFrameworkStores<RestaurantIdentityDbContext>()
                    .AddDefaultTokenProviders();
            #endregion

            #region settings
            service.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            #endregion

            #region repositories
            service.AddTransient<IUserRepository, UserRepository>();
            #endregion

            #region services
            service.AddTransient<IAccountServices, AccountServices>();
            #endregion

            #region Jwt configuration
            service.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters() 
                { 
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ClockSkew = TimeSpan.Zero, 
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                };
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = x =>
                    {
                        x.NoResult();
                        x.Response.StatusCode = 500;
                        x.Response.ContentType = "text/plain";
                        return x.Response.WriteAsync(x.Exception.ToString());
                    },
                    OnChallenge = x =>
                    {
                        x.HandleResponse();
                        x.Response.StatusCode = 401;
                        x.Response.ContentType = "application/json";
                        var response = JsonConvert.SerializeObject(new Response<object>() { Success = false, Error = "You are not authenticared" });
                        return x.Response.WriteAsync(response);
                    },
                    OnForbidden = x =>
                    {
                        x.Response.StatusCode = 403;
                        x.Response.ContentType = "application/json";
                        var response = JsonConvert.SerializeObject(new Response<object>() { Success = false, Error = "You are not authorized to this resource" });
                        return x.Response.WriteAsync(response);
                    }
                };
            });
            #endregion
        }

        public static async Task RunSeedsAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var service  = scope.ServiceProvider;

            var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = service.GetRequiredService<RoleManager<ApplicationRole>>();

            await DefaultRoles.CreateSeed(roleManager);
            await DefaultAdmin.CreateSeed(userManager);
            await DefaultSuperAdmin.CreateSeed(userManager);
        }
    }
}

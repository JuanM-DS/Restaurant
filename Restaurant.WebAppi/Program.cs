using Middleware.Filters;
using Restaurant.Infrastructure.Identity;
using Restaurant.Infrastructure.Persistence;
using Restaurant.Infrastructure.Shared;
using Restaurant.Core.Application;
using Newtonsoft.Json;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region dependency injection
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddApplicatinLayer(builder.Configuration);
builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddIdentityLayer(builder.Configuration);
builder.Services.AddSharedLayer(builder.Configuration);
builder.Services.AddControllers(option => option.Filters.Add<GlobalException>());
builder.Services.AddControllers()
    .AddNewtonsoftJson(option =>
    {
        option.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    });
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Swagger

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "Input your Bearer token in this format "
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "Bearer",
                Name = "Bearer",
                In = ParameterLocation.Header
            }, new List<string>()
        },
    });
});
#endregion


var app = builder.Build();
await app.RunSeedsAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

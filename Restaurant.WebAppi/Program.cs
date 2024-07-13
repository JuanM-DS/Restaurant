using Restaurant.Infrastructure.Persistence;
using Restaurant.Infrastructure.Shared;
using Restaurant.Infrastructure.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region dependency injection
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddServiceIdentityLayer(builder.Configuration);
builder.Services.AddSharedLayer(builder.Configuration);
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
await app.RunSeedsAsync();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

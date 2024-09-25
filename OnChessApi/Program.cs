using OnChessApi.Extensions;
using OnChessApi.Repository;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string corsPlicyName = "OnChessAppPlicy";

// Add services to the container.
IServiceCollection services = builder.Services;

services.AddCors(options =>
{
    options.AddPolicy(corsPlicyName, policy =>
    {
        policy.WithOrigins("http://192.168.100.23", "http://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});

services.AddControllers();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddJwtAuthentication();

builder.Services.AddScoped(sp => new MySqlRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

// Application
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsPlicyName);

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.Run();

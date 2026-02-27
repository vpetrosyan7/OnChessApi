using OnChessApi.ExceptionHandlers;
using OnChessApi.Extensions;
using OnChessApi.Loggers;
using OnChessApi.Repository;

WebApplicationBuilder builder = WebApplication.CreateBuilder();

MySqlRepository repository = new (builder.Configuration.GetConnectionString("DefaultConnection"));

string corsPlicyName = "OnChessAppPlicy";

// Add services to the container.
IServiceCollection services = builder.Services;

services.AddExceptionHandler<CustomExceptionHandler>();
services.AddProblemDetails();

services.AddCors(options =>
{
    options.AddPolicy(corsPlicyName, policy =>
    {
        policy.WithOrigins("http://192.168.100.23", "https://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});

services.AddControllers();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddJwtAuthentication();

builder.Services.AddScoped(sp => repository);

builder.Logging.AddDB(repository);

// Application
WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseException();
app.UseExceptionHandler();

app.UseCors(corsPlicyName);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

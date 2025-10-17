// En MiniBay.API/Program.cs

using MiniBay.Application.Interfaces;
using MiniBay.Infrastructure.Services;
using MiniBay.Application.Features.About;
using MiniBay.Application.Features.Contact;
using Microsoft.EntityFrameworkCore;
using MiniBay.Infrastructure.Data;
using MiniBay.Infrastructure.Persistence;
using MiniBay.Application.Services;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MiniBayDbContext>(options =>
    options.UseSqlServer("Server=localhost,1433;Database=MiniBayDb;User Id=sa;Password=Clave123;TrustServerCertificate=True;"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddControllers();
builder.Services.AddScoped<IAboutService, AboutService>();
builder.Services.AddScoped<IContactService, ContactService>();
// --- CONFIGURACIÓN DE CORS ---
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowBlazorClient",
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost:5195")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// ...
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Asegúrate de que esta línea esté aquí y use el nombre correcto de la política
app.UseCors("AllowBlazorClient");

app.UseAuthorization();
app.MapControllers();

app.Run();
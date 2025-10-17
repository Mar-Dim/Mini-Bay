// En MiniBay.API/Program.cs

using MiniBay.Application.Interfaces;
using MiniBay.Infrastructure.Services;
using MiniBay.Application.Features.About;
using MiniBay.Shared.Features.About;

var builder = WebApplication.CreateBuilder(args);

// ... 
builder.Services.AddControllers();
builder.Services.AddScoped<IAboutService, AboutService>();

// --- CONFIGURACIÓN DE CORS ---
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowBlazorClient",
                      policy  =>
                      {
                          // *** ¡¡LA CORRECCIÓN ESTÁ AQUÍ!! ***
                          // Debes poner la URL del CLIENTE (Blazor), no la de la API.
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
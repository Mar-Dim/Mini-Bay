using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using MiniBay.Application.Features.About;
using MiniBay.Application.Features.Contact;
using MiniBay.Application.Features.Products;
using MiniBay.Application.Interfaces;
using MiniBay.Application.Services;
using MiniBay.Infrastructure.Data;
using MiniBay.Infrastructure.Features.Products;
using MiniBay.Infrastructure.Persistence;
using MiniBay.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MiniBayDbContext>(options =>
    options.UseSqlServer("Server=localhost,1433;Database=MiniBayDb;User Id=sa;Password=Clave123;TrustServerCertificate=True;"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAboutService, AboutService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 10 * 1024 * 1024;
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartHeadersLengthLimit = int.MaxValue;
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 10 * 1024 * 1024;
    options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(2);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.WithOrigins("https://localhost:7289", "http://localhost:5195")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var webRoot = app.Environment.WebRootPath;
if (string.IsNullOrEmpty(webRoot))
{
    webRoot = Path.Combine(app.Environment.ContentRootPath, "wwwroot");
}

var imagesPath = Path.Combine(webRoot, "images", "products");
if (!Directory.Exists(imagesPath))
{
    Directory.CreateDirectory(imagesPath);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowBlazorClient");
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();

app.Run();
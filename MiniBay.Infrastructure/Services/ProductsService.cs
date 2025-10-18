using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MiniBay.Application.DTO;
using MiniBay.Application.Features.Products;
using MiniBay.Domain.Entities;
using MiniBay.Infrastructure.Data;
using MiniBay.Shared.Feature.Products;

namespace MiniBay.Infrastructure.Features.Products
{
    public class ProductService : IProductService
    {
        private readonly MiniBayDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductService(MiniBayDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var products = await _context.Products
                .AsNoTracking()
                .ToListAsync();

            return products.Select(p => new ProductDto
            {
                Id_Pro = p.Id_Pro,
                Nam_Pro = p.Nam_Pro,
                Des_Pro = p.Des_Pro,
                Pri_Pro = p.Pri_Pro,
                Url_Pro = p.Url_Pro
            });
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductsDto productDto, IFormFile? imageFile)
        {
            var newProduct = new Product
            {
                Nam_Pro = productDto.Nam_Pro,
                Des_Pro = productDto.Des_Pro,
                Pri_Pro = productDto.Pri_Pro,
                Url_Pro = null
            };

            if (imageFile != null && imageFile.Length > 0)
            {
                var ext = Path.GetExtension(imageFile.FileName);
                var safeExt = string.IsNullOrWhiteSpace(ext) ? ".jpg" : ext.ToLowerInvariant();

                var webRoot = _env.WebRootPath;
                if (string.IsNullOrWhiteSpace(webRoot))
                {
                    var contentRoot = string.IsNullOrWhiteSpace(_env.ContentRootPath)
                        ? AppContext.BaseDirectory
                        : _env.ContentRootPath;

                    webRoot = Path.Combine(contentRoot, "wwwroot");
                }

                var uploadPath = Path.Combine(webRoot, "images", "products");
                Directory.CreateDirectory(uploadPath);

                var uniqueFileName = $"{Guid.NewGuid():N}{safeExt}";
                var physicalPath = Path.Combine(uploadPath, uniqueFileName);

                try
                {
                    await using var stream = new FileStream(physicalPath, FileMode.Create, FileAccess.Write, FileShare.None);
                    await imageFile.CopyToAsync(stream);
                }
                catch
                {
                    try { if (System.IO.File.Exists(physicalPath)) System.IO.File.Delete(physicalPath); } catch { }
                    throw;
                }

                newProduct.Url_Pro = $"/images/products/{uniqueFileName}";
            }

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return new ProductDto
            {
                Id_Pro = newProduct.Id_Pro,
                Nam_Pro = newProduct.Nam_Pro,
                Des_Pro = newProduct.Des_Pro,
                Pri_Pro = newProduct.Pri_Pro,
                Url_Pro = newProduct.Url_Pro
            };
        }
    }
}

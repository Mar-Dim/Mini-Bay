using Microsoft.AspNetCore.Mvc;
using MiniBay.Application.DTO;
using MiniBay.Application.Features.Products;
using MiniBay.Shared.Feature.Products;
using System;
using System.IO;
using System.Linq;
using MiniBay.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MiniBay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly MiniBayDbContext _context;

        public ProductsController(IProductService productService, MiniBayDbContext context)
        {
            _productService = productService;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var productDto = new ProductDto
            {
                Id_Pro = product.Id_Pro,
                Nam_Pro = product.Nam_Pro,
                Des_Pro = product.Des_Pro,
                Pri_Pro = product.Pri_Pro,
                Url_Pro = product.Url_Pro
            };

            return Ok(productDto);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(
            [FromForm] CreateProductsDto productDto,
            IFormFile? imageFile)
        {
            try
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

                    if (!allowedExtensions.Contains(extension))
                    {
                        return BadRequest("Error: El formato del archivo no es válido. Solo se permite .jpg, .jpeg, .png, o .gif.");
                    }

                    var contentType = imageFile.ContentType.ToLowerInvariant();
                    var allowedContentTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/webp" };

                    if (!allowedContentTypes.Contains(contentType))
                    {
                        return BadRequest("Error: El tipo de archivo no es una imagen válida.");
                    }
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var newProduct = await _productService.CreateProductAsync(productDto, imageFile);

                return CreatedAtAction(nameof(GetProducts), new { id = newProduct.Id_Pro }, newProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> SearchProducts([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return Ok(new List<ProductDto>());
            }

            var products = await _context.Products
                .Where(p => p.Nam_Pro.ToLower().Contains(query.ToLower()))
                .Take(10)
                .AsNoTracking()
                .ToListAsync();

            var productDtos = products.Select(p => new ProductDto
            {
                Id_Pro = p.Id_Pro,
                Nam_Pro = p.Nam_Pro,
                Des_Pro = p.Des_Pro,
                Pri_Pro = p.Pri_Pro,
                Url_Pro = p.Url_Pro
            }).ToList();

            return Ok(productDtos);
        }
    }
}
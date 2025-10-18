using Microsoft.AspNetCore.Mvc;
using MiniBay.Application.DTO;
using MiniBay.Application.Features.Products;
using MiniBay.Shared.Feature.Products;
using System;
using System.IO;   
using System.Linq;


namespace MiniBay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }
        [HttpPost]

        [RequestFormLimits(MultipartBodyLengthLimit = 10485760)]
        [RequestSizeLimit(10485760)]
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
                        return BadRequest("Error: El formato del archivo no es válido. Solo se permite .jpg, .jpeg, .png, .gif o .webp.");
                    }

                    var contentType = imageFile.ContentType.ToLowerInvariant();

                    var allowedContentTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/webp" }; // Añadir image/webp

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
    }
}
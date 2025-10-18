using MiniBay.Application.DTO;
using MiniBay.Shared.Feature.Products;
using Microsoft.AspNetCore.Http;

namespace MiniBay.Application.Features.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<ProductDto> CreateProductAsync(CreateProductsDto productDto, IFormFile? imageFile);
    }
}
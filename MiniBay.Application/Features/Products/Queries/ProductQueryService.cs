// Ubicación: MiniBay.Application/Features/Products/Queries/ProductQueryService.cs

using MiniBay.Application.Contracts.Persistence;
using MiniBay.Application.DTO;
using MiniBay.Domain.Entities;
using System.Threading.Tasks;

namespace MiniBay.Application.Features.Products.Queries
{
    public class ProductQueryService : IProductQueryService
    {
        private readonly IProductRepository _productRepository;

        public ProductQueryService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            
            return product == null ? null : MapEntityToDto(product);
        }

        private ProductDto MapEntityToDto(Product p)
        {
            return new ProductDto
            {
                Id_Pro = p.Id_Pro,
                Nam_Pro = p.Nam_Pro,
                Des_Pro = p.Des_Pro,
                Pri_Pro = p.Pri_Pro,
                Url_Pro = p.Url_Pro
            };
        }
    }
}
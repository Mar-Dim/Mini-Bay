// Ubicación: MiniBay.Application/Features/Products/Queries/IProductQueryService.cs

using MiniBay.Application.DTO;
using System.Threading.Tasks;

namespace MiniBay.Application.Features.Products.Queries
{
       public interface IProductQueryService
    {
              Task<ProductDto> GetProductByIdAsync(int id);
    }
}
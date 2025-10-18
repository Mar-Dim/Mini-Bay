// Ubicación: MiniBay.Infrastructure/Persistence/Repositories/ProductRepository.cs

using MiniBay.Application.Contracts.Persistence;
using MiniBay.Domain.Entities;
using MiniBay.Infrastructure.Data; // Asegúrate que el namespace del DbContext sea correcto
using System.Threading.Tasks;

namespace MiniBay.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MiniBayDbContext _context;

        public ProductRepository(MiniBayDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            // FindAsync es la forma más eficiente de buscar por clave primaria.
            return await _context.Products.FindAsync(id);
        }
    }
}
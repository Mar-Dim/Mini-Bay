
using MiniBay.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBay.Application.Contracts.Persistence
{
    public interface IProductRepository
    {
        
        Task<Product> GetByIdAsync(int id);
}
}
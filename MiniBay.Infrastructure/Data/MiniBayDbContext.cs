using Microsoft.EntityFrameworkCore;
using MiniBay.Domain.Entities;

namespace MiniBay.Infrastructure.Data
{
    public class MiniBayDbContext : DbContext
    {
        public MiniBayDbContext(DbContextOptions<MiniBayDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product>Products { get; set; }
    }
}

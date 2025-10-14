using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MiniBay.Infrastructure.Data
{
    public class MiniBayDbContextFactory : IDesignTimeDbContextFactory<MiniBayDbContext>
    {
        public MiniBayDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MiniBayDbContext>();

            // Conexión al SQL Server de Docker
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=MiniBayDB;User Id=sa;Password=Clave123;TrustServerCertificate=True;");

            return new MiniBayDbContext(optionsBuilder.Options);
        }
    }
}

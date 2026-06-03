using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data
{
    public class ZooDbContextFactory : IDesignTimeDbContextFactory<ZooDbContext>
    {
        public ZooDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddUserSecrets<ZooDbContextFactory>()
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new Exception("Connection string was not found.");

            DbContextOptions<ZooDbContext> options = new DbContextOptionsBuilder<ZooDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            return new ZooDbContext(options);
        }
    }
}
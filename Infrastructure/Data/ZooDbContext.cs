using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ZooDbContext : DbContext
    {
        public ZooDbContext()
        {
        }

        public ZooDbContext(DbContextOptions<ZooDbContext> options) : base(options)
        {
        }

        public DbSet<Animal> Animals { get; set; }

        public DbSet<Habitat> Habitats { get; set; }

        public DbSet<Visitor> Visitors { get; set; }

        public DbSet<Visit> Visits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=FARZAD\\SQLEXPRESS;Database=DjurparkenDb;Trusted_Connection=True;TrustServerCertificate=True;"
                );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
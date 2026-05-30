using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ZooDbContext:DbContext
    {
        public ZooDbContext(DbContextOptions<ZooDbContext> options) : base(options)
        {

        }
        public DbSet<Domain.Entities.Animal> Animals { get; set; }
        public DbSet<Domain.Entities.Habitat> Habitats { get; set; }
        public DbSet<Domain.Entities.Visitor> Visitors { get; set; }
        public DbSet<Domain.Entities.Visit> Visits { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}

using Microsoft.EntityFrameworkCore;

using System.Reflection;

namespace AnemicDomainModelApp.Domain
{
    public class WmsContext : DbContext
    {
        public WmsContext(DbContextOptions<WmsContext> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
        }

        public DbSet<Packing> Packaging { get; set; }
        public DbSet<PackingStatus> PackagingStatus { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductStatus> ProductsStatus { get; set; }
        public DbSet<Unit> Units { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // DATA SEEDING

            modelBuilder.Entity<Unit>().HasData(Unit.AllUnits);

            modelBuilder.Entity<PackingStatus>().HasData(PackingStatus.AllPackingStatus);

            modelBuilder.Entity<ProductStatus>().HasData(ProductStatus.AllProductStatus);
        }
    }
}

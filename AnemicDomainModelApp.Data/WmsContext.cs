using AnemicDomainModelApp.Domain;

using Microsoft.EntityFrameworkCore;

using System.Reflection;

namespace AnemicDomainModelApp.Data
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

            modelBuilder.Entity<Unit>().HasData(new Unit { Id = 1, Value = "CX - Caixa" },
                new Unit { Id = 2, Value = "KG - Quilograma" },
                new Unit { Id = 3, Value = "L - Litro" },
                new Unit { Id = 4, Value = "PC - Peça" },
                new Unit { Id = 5, Value = "PCT - Pacote" },
                new Unit { Id = 6, Value = "UN - Unidade" });

            modelBuilder.Entity<PackingStatus>().HasData(new PackingStatus { Id = 1, Value = "Ativo" },
                new PackingStatus { Id = 2, Value = "Inativo" });

            modelBuilder.Entity<ProductStatus>().HasData(new ProductStatus { Id = 1, Value = "Ativo" },
                new ProductStatus { Id = 2, Value = "Inativo" },
                new ProductStatus { Id = 3, Value = "Bloqueado" });
        }
    }
}

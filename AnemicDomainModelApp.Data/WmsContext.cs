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

            modelBuilder.Entity<Unit>().HasData(new { Id = 1, Value = "CX - Caixa" },
                new { Id = 2, Value = "KG - Quilograma" },
                new { Id = 3, Value = "L - Litro" },
                new { Id = 4, Value = "PC - Peça" },
                new { Id = 5, Value = "PCT - Pacote" },
                new { Id = 6, Value = "UN - Unidade" });

            modelBuilder.Entity<PackingStatus>().HasData(new { Id = 1, Value = "Ativo" },
                new { Id = 2, Value = "Inativo" });

            modelBuilder.Entity<ProductStatus>().HasData(new { Id = 1, Value = "Ativo" },
                new { Id = 2, Value = "Inativo" },
                new { Id = 3, Value = "Bloqueado" });
        }
    }
}

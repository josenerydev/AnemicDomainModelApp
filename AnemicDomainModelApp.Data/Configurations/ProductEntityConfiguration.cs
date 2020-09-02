using AnemicDomainModelApp.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnemicDomainModelApp.Data.Configurations
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(nameof(Product))
                .HasKey(x => x.Id);

            builder.Property(x => x.Description)
                .IsRequired();
            builder.Property(x => x.NetWeight)
                .HasColumnType("decimal(18,3)")
                .IsRequired();

            builder.HasOne(x => x.Unit)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.UnitId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(x => x.ProductStatus)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.ProductStatusId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasMany(x => x.Packaging)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}

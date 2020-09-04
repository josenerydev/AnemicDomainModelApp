using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnemicDomainModelApp.Domain
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(nameof(Product))
                .HasKey(x => x.Id);

            builder.Property(x => x.Description)
                .HasConversion(x => x.Value, x => Description.Create(x).Value)
                .IsRequired();

            builder.Property(x => x.NetWeight)
                .HasConversion(x => x.Value, x => NetWeight.Create(x).Value)
                .HasColumnType("decimal(18,3)")
                .IsRequired();

            builder.HasOne(x => x.Unit)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(x => x.ProductStatus)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasMany(x => x.Packaging)
                .WithOne(x => x.Product)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired()
                .Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}

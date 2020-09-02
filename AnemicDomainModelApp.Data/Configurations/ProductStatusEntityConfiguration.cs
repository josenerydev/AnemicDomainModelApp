using AnemicDomainModelApp.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnemicDomainModelApp.Data.Configurations
{
    public class ProductStatusEntityConfiguration : IEntityTypeConfiguration<ProductStatus>
    {
        public void Configure(EntityTypeBuilder<ProductStatus> builder)
        {
            builder.ToTable(nameof(ProductStatus))
                .HasKey(x => x.Id);

            builder.Property(x => x.Value)
                .IsRequired();

            builder.HasMany(x => x.Products)
                .WithOne(x => x.ProductStatus)
                .HasForeignKey(x => x.ProductStatusId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}

using AnemicDomainModelApp.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnemicDomainModelApp.Data.Configurations
{
    public class UnitEntityConfiguration : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.ToTable(nameof(Unit))
                .HasKey(x => x.Id);

            builder.Property(x => x.Value)
                .IsRequired();

            builder.HasMany(x => x.Products)
                .WithOne(x => x.Unit)
                .HasForeignKey(x => x.UnitId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasMany(x => x.Packaging)
                .WithOne(x => x.Unit)
                .HasForeignKey(x => x.UnitId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}

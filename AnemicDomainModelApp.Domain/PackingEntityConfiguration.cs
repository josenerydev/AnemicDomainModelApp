using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnemicDomainModelApp.Domain
{
    public class PackingEntityConfiguration : IEntityTypeConfiguration<Packing>
    {
        public void Configure(EntityTypeBuilder<Packing> builder)
        {
            builder.ToTable(nameof(Packing))
                .HasKey(x => x.Id);

            builder.Property(x => x.ConvertionFactor)
                .HasConversion(x => x.Value, x => ConversionFactor.Create(x).Value)
                .HasColumnType("decimal(18,3)")
                .IsRequired();

            builder.HasOne(x => x.Unit)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(x => x.PackingStatus)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Packaging)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}

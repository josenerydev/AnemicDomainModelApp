using AnemicDomainModelApp.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnemicDomainModelApp.Data.Configurations
{
    public class PackingEntityConfiguration : IEntityTypeConfiguration<Packing>
    {
        public void Configure(EntityTypeBuilder<Packing> builder)
        {
            builder.ToTable(nameof(Packing))
                .HasKey(x => x.Id);

            builder.Property(x => x.ConvertionFactor)
                .HasColumnType("decimal(18,3)")
                .IsRequired();

            builder.HasOne(x => x.Unit)
                .WithMany(x => x.Packaging)
                .HasForeignKey(x => x.UnitId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(x => x.PackingStatus)
                .WithMany(x => x.Packaging)
                .HasForeignKey(x => x.PackingStatusId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Packaging)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}

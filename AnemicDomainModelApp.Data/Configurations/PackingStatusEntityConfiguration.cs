using AnemicDomainModelApp.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnemicDomainModelApp.Data.Configurations
{
    public class PackingStatusEntityConfiguration : IEntityTypeConfiguration<PackingStatus>
    {
        public void Configure(EntityTypeBuilder<PackingStatus> builder)
        {
            builder.ToTable(nameof(PackingStatus))
                .HasKey(x => x.Id);

            builder.Property(x => x.Value)
                .IsRequired();

            builder.HasMany(x => x.Packaging)
                .WithOne(x => x.PackingStatus)
                .HasForeignKey(x => x.PackingStatusId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}

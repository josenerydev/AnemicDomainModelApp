using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnemicDomainModelApp.Domain
{
    public class PackingStatusEntityConfiguration : IEntityTypeConfiguration<PackingStatus>
    {
        public void Configure(EntityTypeBuilder<PackingStatus> builder)
        {
            builder.ToTable(nameof(PackingStatus))
                .HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired();
        }
    }
}

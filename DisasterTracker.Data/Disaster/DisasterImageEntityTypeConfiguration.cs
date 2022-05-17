using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DisasterTracker.Data.Disaster
{
    public class DisasterImageEntityTypeConfiguration : IEntityTypeConfiguration<DisasterImage>
    {
        public void Configure(EntityTypeBuilder<DisasterImage> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}

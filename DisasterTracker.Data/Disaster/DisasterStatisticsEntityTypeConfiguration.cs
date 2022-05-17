using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DisasterTracker.Data.Disaster
{
    public class DisasterStatisticsEntityTypeConfiguration : IEntityTypeConfiguration<DisasterStatistics>
    {
        public void Configure(EntityTypeBuilder<DisasterStatistics> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}

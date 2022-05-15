using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DisasterTracker.Data.Event
{
    public class EventStatisticsEntityTypeConfiguration : IEntityTypeConfiguration<EventStatistics>
    {
        public void Configure(EntityTypeBuilder<EventStatistics> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}

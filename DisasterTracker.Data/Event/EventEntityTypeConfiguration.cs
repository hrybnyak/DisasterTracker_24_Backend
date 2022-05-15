using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DisasterTracker.Data.Event
{
    public class EventEntityTypeConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => new { e.ApiId, e.LastUpdateDate });
            builder.HasIndex(e => new { e.ApiId });

            builder.Property(e => e.Severity)
                .HasConversion<int>();

            builder.Property(e => e.Type)
                .HasConversion<int>();

            builder.HasMany(e => e.EventStatistics)
                .WithOne(es => es.Event)
                .HasForeignKey(e => e.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.EventImage)
                .WithOne(ei => ei.Event)
                .HasForeignKey<EventImage>(e => e.EventId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

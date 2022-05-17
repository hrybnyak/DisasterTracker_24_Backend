using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DisasterTracker.Data.Disaster
{
    public class DisasterEntityTypeConfiguration : IEntityTypeConfiguration<Disaster>
    {
        public void Configure(EntityTypeBuilder<Disaster> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => new { e.ApiId, e.LastUpdateDate });
            builder.HasIndex(e => new { e.ApiId });

            builder.Property(e => e.ApiId)
                .IsRequired();

            builder.Property(e => e.Severity)
                .HasConversion<int>();

            builder.Property(e => e.Type)
                .HasConversion<int>();

            builder.HasMany(e => e.DisasterStatistics)
                .WithOne(es => es.Disaster)
                .HasForeignKey(e => e.DisasterId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.DisasterImage)
                .WithOne(ei => ei.Disaster)
                .HasForeignKey<DisasterImage>(e => e.DisasterId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

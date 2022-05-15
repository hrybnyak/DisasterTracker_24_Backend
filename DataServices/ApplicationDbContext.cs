using DisasterTracker.Data.Event;
using Microsoft.EntityFrameworkCore;

namespace DisasterTracker.DataServices
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Event> Event { get; set; }
        public DbSet<EventStatistics> EventStatistics { get; set; }
        public DbSet<EventImage> EventImage { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new EventEntityTypeConfiguration().Configure(modelBuilder.Entity<Event>());
            new EventStatisticsEntityTypeConfiguration().Configure(modelBuilder.Entity<EventStatistics>());
            new EventImageEntityTypeConfiguration().Configure(modelBuilder.Entity<EventImage>());
        }

    }
}
using DisasterTracker.Data.Disaster;
using Microsoft.EntityFrameworkCore;

namespace DisasterTracker.DataServices
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Disaster> Disaster { get; set; }
        public DbSet<DisasterStatistics> DisasterStatistics { get; set; }
        public DbSet<DisasterImage> DisasterImage { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new DisasterEntityTypeConfiguration().Configure(modelBuilder.Entity<Disaster>());
            new DisasterStatisticsEntityTypeConfiguration().Configure(modelBuilder.Entity<DisasterStatistics>());
            new DisasterImageEntityTypeConfiguration().Configure(modelBuilder.Entity<DisasterImage>());
        }

    }
}
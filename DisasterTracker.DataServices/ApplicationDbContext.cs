using DisasterTracker.Data.Country;
using DisasterTracker.Data.Disaster;
using DisasterTracker.Data.EmailTemplate;
using DisasterTracker.Data.User;
using Microsoft.EntityFrameworkCore;

namespace DisasterTracker.DataServices
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Disaster> Disaster { get; set; }
        public DbSet<DisasterStatistics> DisasterStatistics { get; set; }
        public DbSet<DisasterImage> DisasterImage { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<CountryDisaster> CountryDisaster { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserLocation> UserLocation { get; set; }
        public DbSet<EmailTemplate> EmailTemplate { get; set; }
        public DbSet<UserPushSubscription> UserPushSubscription { get; set; }
        public DbSet<PushSubscriptionKey> PushSubscriptionKey { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new DisasterEntityTypeConfiguration().Configure(modelBuilder.Entity<Disaster>());
            new DisasterStatisticsEntityTypeConfiguration().Configure(modelBuilder.Entity<DisasterStatistics>());
            new DisasterImageEntityTypeConfiguration().Configure(modelBuilder.Entity<DisasterImage>());
            new CountryEntityTypeConfiguration().Configure(modelBuilder.Entity<Country>());
            new CountryDisasterEntityTypeConfiguration().Configure(modelBuilder.Entity<CountryDisaster>());
            new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<User>());
            new UserLocationEntityTypeConfiguration().Configure(modelBuilder.Entity<UserLocation>());
            new EmailTemplateEntityTypeConfiguration().Configure(modelBuilder.Entity<EmailTemplate>());
            new UserPushSubscriptionEntityTypeConfiguration().Configure(modelBuilder.Entity<UserPushSubscription>());
            new PushSubscriptionKeyEntityTypeConfiguration().Configure(modelBuilder.Entity<PushSubscriptionKey>());
        }

    }
}
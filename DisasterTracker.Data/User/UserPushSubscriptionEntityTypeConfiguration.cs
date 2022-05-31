using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DisasterTracker.Data.User
{
    public class UserPushSubscriptionEntityTypeConfiguration : IEntityTypeConfiguration<UserPushSubscription>
    {
        public void Configure(EntityTypeBuilder<UserPushSubscription> builder)
        {
            builder.HasKey(ps => ps.Id);

            builder.HasMany(ps => ps.PushSubscriptionKeys)
                .WithOne(psk => psk.UserPushSubscription)
                .HasForeignKey(psk => psk.PushSubscriptionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

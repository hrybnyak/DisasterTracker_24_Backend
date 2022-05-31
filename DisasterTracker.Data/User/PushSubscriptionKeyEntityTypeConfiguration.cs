using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DisasterTracker.Data.User
{
    public class PushSubscriptionKeyEntityTypeConfiguration : IEntityTypeConfiguration<PushSubscriptionKey>
    {
        public void Configure(EntityTypeBuilder<PushSubscriptionKey> builder)
        {
            builder.HasKey(psk => psk.Id);
        }
    }
}

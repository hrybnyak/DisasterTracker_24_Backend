using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DisasterTracker.Data.User
{
    public class UserLocationEntityTypeConfiguration : IEntityTypeConfiguration<UserLocation>
    {
        public void Configure(EntityTypeBuilder<UserLocation> builder)
        {
            builder.HasKey(ul => ul.Id);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DisasterTracker.Data.Country
{
    public class CountryDisasterEntityTypeConfiguration : IEntityTypeConfiguration<CountryDisaster>
    {
        public void Configure(EntityTypeBuilder<CountryDisaster> builder)
        {
            builder.HasKey(cd => cd.Id);
        }
    }
}

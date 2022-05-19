using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DisasterTracker.Data.Country
{
    public class CountryEntityTypeConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.Disasters)
                .WithOne(c => c.Country)
                .HasForeignKey(c => c.CountryId);
        }
    }
}

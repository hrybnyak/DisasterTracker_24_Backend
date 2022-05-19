using DisasterTracker.Data.Country;
using Microsoft.Extensions.Logging;

namespace DisasterTracker.DataServices.Repository
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        private readonly ILogger<CountryRepository> _logger;

        public CountryRepository(
            ApplicationDbContext context,
            ILogger<CountryRepository> logger)
            : base(context)
        {
            _logger = logger;
        }

        public Country GetByName(string name)
        {
            try
            {
                var result = _entities.FirstOrDefault(c => c.Name == name);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}

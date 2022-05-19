using DisasterTracker.Data.Country;

namespace DisasterTracker.DataServices.Repository
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        public Country GetByName(string name);
    }
}

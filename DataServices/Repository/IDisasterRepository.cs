using DisasterTracker.Data.Disaster;

namespace DisasterTracker.DataServices.Repository
{
    public interface IDisasterRepository : IGenericRepository<Disaster>
    {
        public Disaster? GetDisasterByApiId(Guid apiId);
        public List<Disaster> GetRelevantDisasters();
        public Disaster? GetFullDisaster(Guid id);
    }
}

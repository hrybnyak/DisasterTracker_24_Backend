using DisasterTracker.Data.Disaster;

namespace DisasterTracker.DataServices.Repository
{
    public interface IDisasterRepository : IGenericRepository<Disaster>
    {
        Disaster? GetDisasterByApiId(Guid apiId);
        List<Disaster> GetRelevantDisasters();
        Disaster? GetFullDisaster(Guid id);
    }
}

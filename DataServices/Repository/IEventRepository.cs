using DisasterTracker.Data.Event;

namespace DisasterTracker.DataServices.Repository
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        public Event? GetEventByApiId(Guid apiId);
    }
}

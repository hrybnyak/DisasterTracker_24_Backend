using DisasterTracker.Data.Event;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DisasterTracker.DataServices.Repository
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        private readonly ILogger<EventRepository> _logger;

        public EventRepository(
            ApplicationDbContext context, 
            ILogger<EventRepository> logger) 
            : base(context)
        {
            _logger = logger;
        }

        public Event? GetEventByApiId(Guid apiId)
        {
            try
            {
                var result = _entities
                    .Include(e => e.EventStatistics)
                    .FirstOrDefault(e => e.ApiId == apiId);

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

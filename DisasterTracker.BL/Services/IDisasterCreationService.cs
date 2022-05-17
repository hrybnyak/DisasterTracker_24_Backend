using DisasterTracker.Data.Disaster;

namespace DisasterTracker.BL.Services
{
    public interface IDisasterCreationService
    {
        public Task<List<Disaster>> CreateOrEditDisasters(CancellationToken stoppingToken);
    }
}

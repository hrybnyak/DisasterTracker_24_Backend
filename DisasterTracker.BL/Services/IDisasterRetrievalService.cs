using DisasterTracker.Data.Disaster;

namespace DisasterTracker.BL.Services
{
    internal interface IDisasterRetrievalService
    {
        Task<(List<Disaster>, List<Disaster>)> CreateOrEditDisasters(CancellationToken stoppingToken);
    }
}

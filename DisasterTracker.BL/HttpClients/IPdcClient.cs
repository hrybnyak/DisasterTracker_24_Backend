using DisasterTracker.PdcApiModels;

namespace DisasterTracker.BL.HttpClients
{
    public interface IPdcClient
    {
        Task<HazardBeans?> GetHazardBeans();
        Task<EventHistory?> GetEventHistory(Guid eventId);
        Task<EventSpecifics?> GetEventSpecifics(Guid eventId, string updateId);
    }
}

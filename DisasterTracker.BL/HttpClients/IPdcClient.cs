using DisasterTracker.PdcApiModels;

namespace DisasterTracker.BL.HttpClients
{
    internal interface IPdcClient
    {
        Task<HazardBeans?> GetHazardBeans();
        Task<DisasterHistory?> GetEventHistory(Guid eventId);
        Task<DisasterSpecifics?> GetEventSpecifics(Guid eventId, string updateId);
    }
}

using DisasterTracker.BL.Dtos;

namespace DisasterTracker.BL.Services
{
    public interface IDisasterService
    {
        List<DisasterDto> GetRelevantDisasters();
        DisasterDto GetDisasterById(Guid id);
    }
}

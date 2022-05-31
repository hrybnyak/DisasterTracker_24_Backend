using DisasterTracker.BL.Dtos;

namespace DisasterTracker.BL.Services.SignalR
{
    public interface ISignalRNotificationService
    {
        Task NotifyAboutNewDisaster(UserDto userDto, UserLocationDto userLocation, DisasterDto disaster);
        Task NotifyAboutUpdatedDisaster(UserDto userDto, UserLocationDto userLocation, DisasterDto disaster);
    }
}

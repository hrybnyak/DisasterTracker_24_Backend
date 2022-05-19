using DisasterTracker.BL.Dtos;

namespace DisasterTracker.BL.Services
{
    internal interface INotificationService
    {
        Task NotifyAboutCreatedDisaster(UserDto userDto, UserLocationDto userLocation, DisasterDto disasterDto);
        Task NotifyAboutUpdatedDisaster(UserDto userDto, UserLocationDto userLocation, DisasterDto disasterDto);
    }
}

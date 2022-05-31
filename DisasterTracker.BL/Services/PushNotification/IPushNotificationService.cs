using DisasterTracker.BL.Dtos;

namespace DisasterTracker.BL.Services.PushNotification
{
    public interface IPushNotificationService
    {
        Task NotifyAboutDisaster(UserDto userDto, UserLocationDto userLocation, DisasterDto disaster);
    }
}

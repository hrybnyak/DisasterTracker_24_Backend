using DisasterTracker.BL.Dtos;

namespace DisasterTracker.BL.Services.EmailNotification
{
    public interface IMailNotificationService
    {
        Task SendNewDisasterEmail(UserDto userDto, UserLocationDto userLocation, DisasterDto disasterDto);
        Task SendUpdatedDisasterEmail(UserDto userDto, UserLocationDto userLocation, DisasterDto disasterDto);
    }
}

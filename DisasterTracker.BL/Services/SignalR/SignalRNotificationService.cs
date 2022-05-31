using DisasterTracker.BL.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace DisasterTracker.BL.Services.SignalR
{
    public class SignalRNotificationService : ISignalRNotificationService
    {
        private readonly IHubContext<DisasterNotificationHub> _hubContext;

        public SignalRNotificationService(IHubContext<DisasterNotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyAboutNewDisaster(UserDto userDto, UserLocationDto userLocation, DisasterDto disaster) =>
            await _hubContext.Clients.User(userDto.Id.ToString()).SendAsync("NewDisaster",  new
            {
                User = userDto,
                UserLocation = userLocation,
                Disaster = disaster
            });

        public async Task NotifyAboutUpdatedDisaster(UserDto userDto, UserLocationDto userLocation, DisasterDto disaster) =>
            await _hubContext.Clients.User(userDto.Id.ToString()).SendAsync("UpdatedDisaster", new
            {
                User = userDto,
                UserLocation = userLocation,
                Disaster = disaster
            });
    }
}

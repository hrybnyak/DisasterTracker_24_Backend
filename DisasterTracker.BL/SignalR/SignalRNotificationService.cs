using DisasterTracker.BL.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace DisasterTracker.BL.SignalR
{
    internal class SignalRNotificationService : ISignalRNotificationService
    {
        private readonly IHubContext<DisasterNotificationHub> _hubContext;

        public SignalRNotificationService(IHubContext<DisasterNotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyAboutNewDisaster(UserDto userDto, UserLocationDto userLocation, DisasterDto disaster) =>
            await _hubContext.Clients.User(userDto.Id.ToString()).SendAsync("NewDisaster", userDto, userLocation, disaster);

        public async Task NotifyAboutUpdatedDisaster(UserDto userDto, UserLocationDto userLocation, DisasterDto disaster) =>
            await _hubContext.Clients.User(userDto.Id.ToString()).SendAsync("UpdatedDisaster", userDto, userLocation, disaster);
    }
}

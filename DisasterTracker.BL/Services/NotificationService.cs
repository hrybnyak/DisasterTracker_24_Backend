using DisasterTracker.BL.Dtos;
using DisasterTracker.BL.Services.EmailNotification;
using DisasterTracker.BL.Services.PushNotification;
using DisasterTracker.BL.Services.SignalR;
using Microsoft.Extensions.Logging;

namespace DisasterTracker.BL.Services
{
    internal class NotificationService : INotificationService
    {
        private readonly ISignalRNotificationService _signalRNotificationService;
        private readonly IMailNotificationService _mailNotificationService;
        private readonly IPushNotificationService _pushNotificationService;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(ISignalRNotificationService signalRNotificationService,
            IMailNotificationService mailNotificationService,
            ILogger<NotificationService> logger,
            IPushNotificationService pushNotificationService)
        {
            _signalRNotificationService = signalRNotificationService;
            _mailNotificationService = mailNotificationService;
            _logger = logger;
            _pushNotificationService = pushNotificationService;
        }

        public async Task NotifyAboutCreatedDisaster(UserDto userDto, UserLocationDto userLocation, DisasterDto disasterDto)
        {
            try
            {
                if (userDto.ReceivePushNotifications)
                {
                    await _signalRNotificationService.NotifyAboutNewDisaster(userDto, userLocation, disasterDto);
                    await _pushNotificationService.NotifyAboutDisaster(userDto, userLocation, disasterDto);
                }

                if (userDto.ReceiveEmails)
                {
                    await _mailNotificationService.SendNewDisasterEmail(userDto, userLocation, disasterDto);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public async Task NotifyAboutUpdatedDisaster(UserDto userDto, UserLocationDto userLocation, DisasterDto disasterDto)
        {
            try
            {
                if (userDto.ReceivePushNotifications)
                {
                    await _signalRNotificationService.NotifyAboutUpdatedDisaster(userDto, userLocation, disasterDto);
                    await _pushNotificationService.NotifyAboutDisaster(userDto, userLocation, disasterDto);
                }

                if (userDto.ReceiveEmails)
                {
                    await _mailNotificationService.SendUpdatedDisasterEmail(userDto, userLocation, disasterDto);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}

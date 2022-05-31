using AutoMapper;
using DisasterTracker.BL.Dtos;
using DisasterTracker.BL.Extensions;
using DisasterTracker.DataServices.Repository;
using Microsoft.Extensions.Logging;

namespace DisasterTracker.BL.Services
{
    internal class NotificationOrchestrator : INotificationOrchestrator
    {
        private readonly ILogger<NotificationOrchestrator> _logger;
        private readonly IUserRepository _userRepository;
        private readonly INotificationService _notificationService;
        private readonly IDisasterRetrievalService _disasterCreationService;
        private readonly IMapper _mapper;

        public NotificationOrchestrator(
            ILogger<NotificationOrchestrator> logger, 
            IUserRepository userRepository, 
            INotificationService notificationService, 
            IDisasterRetrievalService disasterCreationService, 
            IMapper mapper)
        {
            _logger = logger;
            _userRepository = userRepository;
            _notificationService = notificationService;
            _disasterCreationService = disasterCreationService;
            _mapper = mapper;
        }

        public async Task CoordinateUserNotification(CancellationToken stoppingToken)
        {
            CheckIfCancellationTokenWasCancelled(stoppingToken);
            var users = GetUsersToNotify(stoppingToken);
            var (disastersCreated, disastersUpdated) = await GetDisastersToNotifyAbout(stoppingToken);
            NotifyUsersAboutDisasters(stoppingToken, disastersCreated, users, _notificationService.NotifyAboutCreatedDisaster);
            NotifyUsersAboutDisasters(stoppingToken, disastersUpdated, users, _notificationService.NotifyAboutUpdatedDisaster);

        }

        private List<UserDto> GetUsersToNotify(CancellationToken stoppingToken)
        {
            try
            {
                CheckIfCancellationTokenWasCancelled(stoppingToken);
                
                var users = _userRepository.GetUsersToNotify();
                return _mapper.Map<List<UserDto>>(users);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "There was an error during user retrieval from database");
                throw;
            }
        }

        private async Task<(List<DisasterDto>, List<DisasterDto>)> GetDisastersToNotifyAbout(CancellationToken stoppingToken)
        {
            var (disastersCreated, disastersEdited) = await _disasterCreationService.CreateOrEditDisasters(stoppingToken);
            return (_mapper.Map<List<DisasterDto>>(disastersCreated), _mapper.Map<List<DisasterDto>>(disastersEdited));
        }

        private void NotifyUsersAboutDisasters(
            CancellationToken stoppingToken,
            List<DisasterDto> disasters, 
            List<UserDto> users, 
            Func<UserDto, UserLocationDto, DisasterDto, Task> notify)
        {
            try
            {
                CheckIfCancellationTokenWasCancelled(stoppingToken);
                Parallel.ForEach(users, async (user) =>
                {
                    
                    foreach (var userLocation in user.Locations)
                    {
                        foreach (var disaster in disasters)
                        {
                            CheckIfCancellationTokenWasCancelled(stoppingToken);
                            if (disaster.ShouldNotifyUserAboutDisaster(userLocation))
                            {
                                await notify(user, userLocation, disaster);
                            }
                        }
                    }
                });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private void CheckIfCancellationTokenWasCancelled(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                _logger.LogError("Task was cancelled.");
                cancellationToken.ThrowIfCancellationRequested();
            }
        }

    }
}

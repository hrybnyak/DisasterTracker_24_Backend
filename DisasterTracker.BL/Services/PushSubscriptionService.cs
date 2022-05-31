using AutoMapper;
using DisasterTracker.Data.User;
using DisasterTracker.DataServices.Repository;
using Lib.Net.Http.WebPush;
using Microsoft.Extensions.Logging;

namespace DisasterTracker.BL.Services
{
    internal class PushSubscriptionService : IPushSubscriptionService
    {
        private readonly ILogger<PushSubscriptionService> _logger;
        private readonly IUserPushSubscriptionRepository _pushSubscriptionRepository;
        private readonly IMapper _mapper;

        public PushSubscriptionService(ILogger<PushSubscriptionService> logger, IUserPushSubscriptionRepository pushSubscriptionRepository, IMapper mapper)
        {
            _logger = logger;
            _pushSubscriptionRepository = pushSubscriptionRepository;
            _mapper = mapper;
        }

        public async Task AddPushSubscriptionToUser(Guid userId, PushSubscription pushSubscription)
        {
            try
            {
                var existingSubscription = _pushSubscriptionRepository.GetPushSubscriptionByUserIdAndEndpoint(userId, pushSubscription.Endpoint);
                if (existingSubscription != null)
                {
                    var message = "The subscription for this user with this endpoint already exist, please remove previous subscription";
                    _logger.LogError(message);
                    throw new ArgumentException(message);
                }

                var userPushSubscription = _mapper.Map<UserPushSubscription>(pushSubscription);
                userPushSubscription.Id = Guid.NewGuid();
                userPushSubscription.UserId = userId;
                userPushSubscription.CreatedOn = DateTime.UtcNow;
                userPushSubscription.ModifiedOn = DateTime.UtcNow;
                await _pushSubscriptionRepository.Insert(userPushSubscription, true);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task DeleteSubscription(Guid userId)
        {
            try
            {
                var existingSubscriptions = _pushSubscriptionRepository.GetPushSubscriptionsByUserId(userId);
                if (existingSubscriptions == null || !existingSubscriptions.Any())
                {
                    var message = "The subscription for this user doesn't exist";
                    _logger.LogError(message);
                    throw new ArgumentException(message);
                }

                foreach (var existingSubscription in existingSubscriptions)
                {
                    await _pushSubscriptionRepository.Delete(existingSubscription, true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}

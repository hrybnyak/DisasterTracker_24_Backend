using AutoMapper;
using DisasterTracker.BL.Dtos;
using DisasterTracker.DataServices.Repository;
using Lib.Net.Http.WebPush;
using Lib.Net.Http.WebPush.Authentication;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DisasterTracker.BL.Services.PushNotification
{
    internal class PushNotificationService : IPushNotificationService
    {
        private readonly IMapper _mapper;
        private readonly IUserPushSubscriptionRepository _pushSubscriptionRepository;
        private readonly PushServiceClient _pushClient;

        public PushNotificationService(
            IMapper mapper,
            IUserPushSubscriptionRepository pushSubscriptionRepository,
            PushServiceClient pushClient,
            IOptions<PushNotificationsOptions> options
            )
        {
            _mapper = mapper;
            _pushSubscriptionRepository = pushSubscriptionRepository;
            _pushClient = pushClient;
            _pushClient.DefaultAuthentication = new VapidAuthentication(options.Value.PublicKey, options.Value.PrivateKey);
        }

        public async Task NotifyAboutDisaster(UserDto userDto, UserLocationDto userLocation, DisasterDto disaster)
        {
            var subscriptions = GetPushSubscriptions(userDto.Id);
            var notification = new PushMessage(JsonConvert.SerializeObject(new
            {
                User = userDto,
                UserLocation = userLocation,
                Disaster = disaster
            }));
            foreach (var subscription in subscriptions)
            {
                await _pushClient.RequestPushMessageDeliveryAsync(subscription, notification);
            }
        }

        private List<PushSubscription> GetPushSubscriptions(Guid userId)
        {
            var subscription = _pushSubscriptionRepository.GetPushSubscriptionsByUserId(userId);
            return _mapper.Map<List<PushSubscription>>(subscription);
        }
    }
}

using Lib.Net.Http.WebPush;

namespace DisasterTracker.BL.Services
{
    public interface IPushSubscriptionService
    {
        Task AddPushSubscriptionToUser(Guid userId, PushSubscription pushSubscription);
        Task DeleteSubscription(Guid userId);
    }
}

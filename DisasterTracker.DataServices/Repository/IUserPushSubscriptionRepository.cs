using DisasterTracker.Data.User;

namespace DisasterTracker.DataServices.Repository
{
    public interface IUserPushSubscriptionRepository : IGenericRepository<UserPushSubscription>
    {
        UserPushSubscription? GetPushSubscriptionByUserIdAndEndpoint(Guid userId, string endpoint);
        List<UserPushSubscription> GetPushSubscriptionsByUserId(Guid userId);
    }
}

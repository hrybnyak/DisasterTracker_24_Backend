using DisasterTracker.Data.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DisasterTracker.DataServices.Repository
{
    internal class UserPushSubscriptionRepository : GenericRepository<UserPushSubscription>, IUserPushSubscriptionRepository
    {
        private readonly ILogger<UserPushSubscriptionRepository> _logger;


        public UserPushSubscriptionRepository(ApplicationDbContext context,
            ILogger<UserPushSubscriptionRepository> logger) : base(context)
        {
            _logger = logger;
        }

        public UserPushSubscription? GetPushSubscriptionByUserIdAndEndpoint(Guid userId, string endpoint)
        {
            try
            {
                return _entities.FirstOrDefault(ps => ps.UserId == userId && ps.Endpoint == endpoint);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public List<UserPushSubscription> GetPushSubscriptionsByUserId(Guid userId)
        {
            try
            {
                return _entities
                    .Include(ps => ps.PushSubscriptionKeys)
                    .Where(ps => ps.UserId == userId)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}

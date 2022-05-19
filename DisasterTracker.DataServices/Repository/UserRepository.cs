using DisasterTracker.Data.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DisasterTracker.DataServices.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(
            ApplicationDbContext context,
            ILogger<UserRepository> logger)
            : base(context)
        {
            _logger = logger;
        }

        public User? GetFullUser(Guid id)
        {
            try
            {
                var result = _entities
                    .Include(u => u.Locations)
                    .FirstOrDefault(d => d.Id == id);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public User? GetFullUserByEmail(string email)
        {
            {
                try
                {
                    var result = _entities
                        .Include(u => u.Locations)
                        .SingleOrDefault(d => d.Email == email);

                    return result;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    throw;
                }
            }
        }

        public List<User> GetUsersToNotify()
        {
            try
            {
                var result = _entities
                    .Include(u => u.Locations)
                    .Where(u => u.RecievePushNotifications || u.ReceiveEmails)
                    .ToList();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}

using DisasterTracker.Data.User;

namespace DisasterTracker.DataServices.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User? GetFullUser(Guid id);
        User? GetFullUserByEmail(string email);
        List<User> GetUsersToNotify();
    }
}

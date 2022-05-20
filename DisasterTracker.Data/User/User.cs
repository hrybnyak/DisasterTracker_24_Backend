using Microsoft.AspNetCore.Identity;

namespace DisasterTracker.Data.User
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string? UserName { get; set; }
        public bool ReceivePushNotifications { get; set; }
        public bool ReceiveEmails { get; set; }

        public List<UserLocation> Locations { get; set; } = new List<UserLocation>();
    }
}

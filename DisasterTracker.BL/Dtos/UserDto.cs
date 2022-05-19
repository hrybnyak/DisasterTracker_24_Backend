using System.ComponentModel.DataAnnotations;

namespace DisasterTracker.BL.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string UserName { get; set; }

        public bool RecievePushNotifications { get; set; }
        public bool ReceiveEmails { get; set; }

        public IEnumerable<UserLocationDto> Locations { get; set; }
    }
}

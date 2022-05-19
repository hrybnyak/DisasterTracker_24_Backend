using System.ComponentModel.DataAnnotations;

namespace DisasterTracker.BL.Dtos
{
    public class CreateUserDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public string? UserName { get; set; }    
    }
}

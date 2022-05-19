using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterTracker.Data.User
{
    public class UserLocation : BaseEntity
    {
        public Guid UserId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int Distance { get; set; }
        public string? Title { get; set; }
        public string? Label { get; set; }
        public User User { get; set; }
    }
}

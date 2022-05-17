using System.ComponentModel.DataAnnotations.Schema;

namespace DisasterTracker.Data.Disaster
{
    public class Disaster : BaseEntity
    {
        public Guid ApiId { get; set; }
        public string? UpdateId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DisasterType Type { get; set; }
        public Severity Severity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public bool AutoExpire { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public IEnumerable<DisasterStatistics> DisasterStatistics { get; set; } = new List<DisasterStatistics>();
        public DisasterImage? DisasterImage { get; set; }
    }
}


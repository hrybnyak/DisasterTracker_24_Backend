using DisasterTracker.Data.Disaster;

namespace DisasterTracker.BL.Dtos
{
    public class DisasterDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DisasterType Type { get; set; }
        public Severity Severity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public IEnumerable<DisasterStatisticsDto> DisasterStatistics { get; set; } = new List<DisasterStatisticsDto>();
        public DisasterImageDto? DisasterImage { get; set; }
    }
}

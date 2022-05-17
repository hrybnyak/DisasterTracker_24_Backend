namespace DisasterTracker.Data.Disaster
{
    public class DisasterImage : BaseEntity
    {
        public Guid DisasterId { get; set; }
        public string? MapImageAddress { get; set; }
        public string? InfrastructureMapImageAddress { get; set; }
        public string? OverviewMapImageAddress { get; set; }

        public Disaster? Disaster { get; set; }
    }
}

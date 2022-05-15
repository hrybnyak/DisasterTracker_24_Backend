namespace DisasterTracker.Data.Event
{
    public class EventImage : BaseEntity
    {
        public Guid EventId { get; set; }
        public string? MapImageAddress { get; set; }
        public string? InfrastructureMapImageAddress { get; set; }
        public string? OverviewMapImageAddress { get; set; }

        public Event? Event { get; set; }
    }
}

namespace DisasterTracker.Data.Event
{
    public class Event : BaseEntity
    {
        public Guid ApiId { get; set; }
        public long UpdateId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public EventType Type { get; set; }
        public Severity Severity { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public DateTimeOffset LastUpdateDate { get; set; }
        public bool AutoExpire { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public IEnumerable<EventStatistics> EventStatistics { get; set; } = new List<EventStatistics>();
        public EventImage? EventImage { get; set; }
    }
}


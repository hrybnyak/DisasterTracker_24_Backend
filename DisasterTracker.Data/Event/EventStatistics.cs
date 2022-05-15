namespace DisasterTracker.Data.Event
{
    public class EventStatistics : BaseEntity
    {
        public Guid EventId { get; set; }
        public int? Population0_14Affected { get; set; }
        public int? Population15_64Affected { get; set; }
        public int? PopulationAbove65Affected { get; set; }
        public int? TotalPopulation { get; set; }
        public long? CapitalExposed { get; set; }
        public int? Hospitals { get; set; }
        public int? Schools { get; set; }
        public int? Households { get; set; }
        public string? Severity { get; set; }

        public Event? Event { get; set; }
    }
}

namespace DisasterTracker.Data.Country
{
    public class CountryDisaster : BaseEntity
    {
        public Guid CountryId { get; set; } 
        public Guid DisasterId { get; set; }
         
        public int AffectedPopulation { get; set; }

        public Disaster.Disaster Disaster { get; set; }
        public Country Country { get; set; }
    }
}

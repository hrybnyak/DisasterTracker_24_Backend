namespace DisasterTracker.Data.Country
{
    public class Country : BaseEntity
    {
        public string ISO3 { get; set; }

        public string Name { get; set; }

        public string LongName { get; set; }

        public IEnumerable<CountryDisaster> Disasters { get; set; } = new List<CountryDisaster>();
    }
}

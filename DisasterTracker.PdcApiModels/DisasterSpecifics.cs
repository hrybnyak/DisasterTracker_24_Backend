using Newtonsoft.Json;

namespace DisasterTracker.PdcApiModels
{
    public class Value
    {
        [JsonProperty("Age 0-14")]
        public int? Population0_14Affected { get; set; }

        [JsonProperty("Age 15-64")]
        public int? Population15_64Affected { get; set; }

        [JsonProperty("Age 65+")]
        public int? PopulationAbove65Affected { get; set; }

        [JsonProperty("total population")]
        public int? TotalPopulation { get; set; }

        [JsonProperty("Schools")]
        public int? Schools { get; set; }

        [JsonProperty("Exposure Area")]
        public string? ExposureArea { get; set; }

        [JsonProperty("Hospitals")]
        public int? Hospitals { get; set; }

        [JsonProperty("Household")]
        public int? Households { get; set; }

        [JsonProperty("Capital Exposed")]
        public long CapitalExposed { get; set; }

        [JsonProperty("Severity")]
        public string? Severity { get; set; }

        [JsonProperty("Distance")]
        public string? Distance { get; set; }
    }

    public class ValueWrapper
    {
        public Value? Values { get; set; }
    }

    public class PopulationAffectedByCountries
    {
        public string? Country { get; set; }
        public int? Year { get; set; }
        public int? Total { get; set; }
    }

    public class Aims
    {
        public List<ValueWrapper> Data { get; set; } = new List<ValueWrapper>();
    }

    public class Populations
    {
        public List<PopulationAffectedByCountries> Data { get; set; } = new List<PopulationAffectedByCountries>();
    } 

    public class DisasterSpecifics
    {
        public Populations? Populations { get; set; }
        public Aims? Aims { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace ApiProject.Models
{
    public class PopulationResponse
    {
        [JsonPropertyName("data")]
        public List<PopulationData> Data { get; set; }
    }

    // Submodel representing individual population data
    public class PopulationData
    {
        // The name of the nation
        [JsonPropertyName("Nation")]
        public string Nation { get; set; }

        // The population of the nation
        [JsonPropertyName("Population")]
        public int Population { get; set; }
    }
}

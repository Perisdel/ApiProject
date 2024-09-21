using System.Text.Json.Serialization;

namespace ApiProject.Models
{
    public class CatFactResponse
    {
        // The actual cat fact returned by the API
        [JsonPropertyName("fact")]
        public string Fact { get; set; }

        // The length of the cat fact string
        [JsonPropertyName("length")]
        public int Length { get; set; }
    }
}

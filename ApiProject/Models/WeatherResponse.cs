using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiProject.Models
{
    public class WeatherResponse
    {

        // Information about the city from the weather API
        [JsonPropertyName("city")]
        public City City { get; set; }

        // List of weather data points (e.g., forecast for multiple days)
        [JsonPropertyName("list")]
        public List<WeatherData> List { get; set; }
    }

    // Submodel for city information
    public class City
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    // Submodel for individual weather data 
    public class WeatherData
    {
        // Date and time of the forecast data
        [JsonPropertyName("dt_txt")]
        public string Date { get; set; }

        // Main weather data 
        [JsonPropertyName("main")]
        public Main Main { get; set; }
    }

    // Submodel for main weather attributes 
    public class Main
    {
        // Temperature data
        [JsonPropertyName("temp")]
        public float Temperature { get; set; }
    }
}

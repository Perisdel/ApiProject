using System.Diagnostics;
using System.Text.Json;
using ApiProject.Models;

namespace YourNamespace.Services
{
    public class ConsolidatedApiService
    {
        // HttpClient for making external API calls
        private readonly HttpClient _httpClient;
        // Logger for logging errors and warnings
        private readonly ILogger<ConsolidatedApiService> _logger;

        public ConsolidatedApiService(HttpClient httpClient, ILogger<ConsolidatedApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<(CatFactResponse catFact, PopulationResponse population, WeatherResponse weather, PerformanceMetrics metrics)> GetConsolidatedDataAsync()
        {
            // Measure the time for each API call
            var catFactTiming = MeasureApiTiming(FetchCatFactAsync);
            var populationTiming = MeasureApiTiming(FetchPopulationAsync);
            var weatherTiming = MeasureApiTiming(FetchWeatherAsync);

            // Await all three tasks asynchronously
            await Task.WhenAll(catFactTiming, populationTiming, weatherTiming);

            // Capture results and their elapsed times
            var catFactResult = await catFactTiming;
            var populationResult = await populationTiming;
            var weatherResult = await weatherTiming;

            // Total elapsed time
            var totalElapsedTime = catFactResult.time + populationResult.time + weatherResult.time;

            // Create performance metrics
            var metrics = new PerformanceMetrics
            {
                CatFactTime = CategorizeTime(catFactResult.time),
                PopulationTime = CategorizeTime(populationResult.time),
                WeatherTime = CategorizeTime(weatherResult.time),
                TotalTime = totalElapsedTime
            };

            // Return API data and metrics
            return (catFactResult.data, populationResult.data, weatherResult.data, metrics);
        }
        // Helper function to categorize time
        private string CategorizeTime(long timeInMillis)
        {
            if (timeInMillis < 100)
                return $"Fast ({timeInMillis} ms)";
            if (timeInMillis <= 200)
                return $"Average ({timeInMillis} ms)";
            return $"Slow ({timeInMillis} ms)";
        }

        // Helper method to measure API timing and data fetching
        private async Task<(T data, long time)> MeasureApiTiming<T>(Func<Task<T>> apiCall)
        {
            var stopwatch = Stopwatch.StartNew();  // Start the timer
            var data = await apiCall();  // Call the API
            stopwatch.Stop();  // Stop the timer
            return (data, stopwatch.ElapsedMilliseconds);  // Return the result and elapsed time
        }


        // Fetches a random cat fact from the CatFact API
        private async Task<CatFactResponse?> FetchCatFactAsync()
        {
            var apiUrl = "https://catfact.ninja/fact";

            try
            {
                var response = await _httpClient.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("CatFact API returned non-success status: {StatusCode}", response.StatusCode);
                    return null;
                }
                // Read and deserialize the response content
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<CatFactResponse>(content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching cat fact.");
                return null;
            }
        }

        // Fetches U.S. population data from the Population API
        private async Task<PopulationResponse> FetchPopulationAsync()
        {
            var apiUrl = "https://datausa.io/api/data?drilldowns=Nation&measures=Population";

            try
            {
                var response = await _httpClient.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Population API returned non-success status: {StatusCode}", response.StatusCode);
                    return null;
                }

                // Deserialize the response content
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<PopulationResponse>(content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching population data.");
                return null;
            }
        }

        // Fetches weather forecast data from the OpenWeatherMap API
        private async Task<WeatherResponse> FetchWeatherAsync()
        {
            var apiUrl = "http://api.openweathermap.org/data/2.5/forecast?id=524901&appid=96ec15b308bdac142df51c4041058aa1";

            try
            {
                var response = await _httpClient.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Weather API returned non-success status: {StatusCode}", response.StatusCode);
                    return null;
                }

                // Deserialize the response content
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<WeatherResponse>(content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching weather data.");
                return null;
            }
        }

        // Class to store performance metrics
        public class PerformanceMetrics
        {
            public string CatFactTime { get; set; }
            public string PopulationTime { get; set; }
            public string WeatherTime { get; set; }
            public long TotalTime { get; set; }
        }
    }
}

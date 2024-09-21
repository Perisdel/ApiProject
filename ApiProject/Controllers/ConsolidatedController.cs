using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YourNamespace.Services;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsolidatedController : ControllerBase
    {
        private readonly ConsolidatedApiService _consolidatedApiService;

        public ConsolidatedController(ConsolidatedApiService consolidatedApiService)
        {
            _consolidatedApiService = consolidatedApiService;
        }

        // GET: /api/consolidated/consolidated-data
        // Returns a consolidated response from all three APIs
        [HttpGet("consolidated-data")]
        public async Task<IActionResult> GetConsolidatedData()
        {
            // Fetch data from all three APIs
            var (catFact, population, weather, metrics) = await _consolidatedApiService.GetConsolidatedDataAsync();

            // Return a 500 status code if any of the APIs fail
            if (catFact == null || population == null || weather == null)
            {
                return StatusCode(500, "Failed to fetch data from one or more external APIs.");
            }

            // Return a unified JSON object containing data from all APIs and each time request performance
            var result = new
            {
                CatFact = catFact,
                Population = population,
                Weather = weather,
                Performance = metrics
            };

            // Return HTTP 200 status with the data
            return Ok(result);
        }
    }
}

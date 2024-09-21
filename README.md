API Overview:
This is a consolidated API service retrieves data from three external APIs and returns the results through a single endpoint.

External APIs Used:

1)
Cat Fact API: Provides random facts about cats.
Endpoint: https://catfact.ninja/fact
Response example:
{
  "fact": "Cats have five toes on their front paws but only four on their back ones.",
  "length": 75
}
  
2)
Population API: Provides U.S. population data.
Endpoint: https://datausa.io/api/data?drilldowns=Nation&measures=Population
Response example:
{
"data": [
    {
      "Nation": "United States",
      "Population": 331883986
    }
  ]
}

3)
OpenWeatherMap API: Provides weather forecast data.

Endpoint: http://api.openweathermap.org/data/2.5/forecast?id=524901&appid=YOUR_API_KEY
Response example:
{
  "city": { "name": "Moscow" },
  "list": [
    {
      "dt_txt": "2024-09-21 12:00:00",
      "main": { "temp": 289.5 }
    }
  ]
}

There is a unified end point for the user 
Unified Endpoint:
URL: /api/consolidated/consolidated-data
Method: GET
Description: This endpoint aggregates data from the three external APIs and each api call time stamp

using Microsoft.AspNetCore.Mvc;

namespace PollyClientWebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientFactory httpClientFactory)
    : ControllerBase
{
    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<ActionResult<IEnumerable<WeatherForecast>>> Get()
    {
        var httpClient = httpClientFactory.CreateClient("PollyServerWebApi");
        var response = await httpClient.GetAsync("/WeatherForecast");
        if (response.IsSuccessStatusCode)
        {
            var weatherForecasts = await response.Content.ReadFromJsonAsync<IEnumerable<WeatherForecast>>();
            return Ok(weatherForecasts);
        }

        return StatusCode((int)response.StatusCode);
    }
}

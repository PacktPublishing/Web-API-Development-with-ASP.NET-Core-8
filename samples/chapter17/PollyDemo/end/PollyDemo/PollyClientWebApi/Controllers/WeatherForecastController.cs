using Microsoft.AspNetCore.Mvc;

using Polly;

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

        var pollyPipeline = new ResiliencePipelineBuilder()
        .AddRetry(new Polly.Retry.RetryStrategyOptions()
        {
            ShouldHandle = new PredicateBuilder().Handle<Exception>(),
            MaxRetryAttempts = 3,
            Delay = TimeSpan.FromMilliseconds(500),
            BackoffType = DelayBackoffType.Exponential,
            MaxDelay = TimeSpan.FromSeconds(5),
            OnRetry = args =>
            {
                logger.LogWarning($"Retry {args.AttemptNumber}, due to: {args.Outcome.Exception?.Message}.");
                return default;
            }
        })
        .Build();

        HttpResponseMessage? response = null;

        await pollyPipeline.ExecuteAsync(async _ =>
        {
            response = await httpClient.GetAsync("/WeatherForecast");
            response.EnsureSuccessStatusCode();
        });

        if (response != null & response!.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<WeatherForecast>>();
            return Ok(result);
        }

        return StatusCode((int)response.StatusCode, response.ReasonPhrase);
    }
}

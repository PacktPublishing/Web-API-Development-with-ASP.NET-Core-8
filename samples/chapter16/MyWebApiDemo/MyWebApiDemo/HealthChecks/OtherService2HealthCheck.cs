using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MyWebApiDemo.HealthChecks;

public class OtherService2HealthCheck : IHealthCheck
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly Random _random = new();

    public OtherService2HealthCheck(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }


    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        // Use random to simulate a 50% chance of success.
        if (_random.Next(0, 2) == 0)
        {
            return HealthCheckResult.Unhealthy("A unhealthy result.");
        }
        var client = _httpClientFactory.CreateClient("JsonPlaceholder");
        var response = await client.GetAsync("users", cancellationToken);
        return response.IsSuccessStatusCode
            ? HealthCheckResult.Healthy("A healthy result.")
            : HealthCheckResult.Unhealthy("An unhealthy result.");
    }
}

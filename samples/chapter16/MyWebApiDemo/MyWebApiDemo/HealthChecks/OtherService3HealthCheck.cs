using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MyWebApiDemo.HealthChecks;

public class OtherService3HealthCheck(IHttpClientFactory httpClientFactory) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        var client = httpClientFactory.CreateClient("JsonPlaceholder");
        var response = await client.GetAsync("comments", cancellationToken);
        return response.IsSuccessStatusCode
            ? HealthCheckResult.Healthy("A healthy result.")
            : HealthCheckResult.Unhealthy("An unhealthy result.");
    }
}

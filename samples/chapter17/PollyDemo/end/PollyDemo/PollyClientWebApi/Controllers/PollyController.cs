using Microsoft.AspNetCore.Mvc;
using Polly.Registry;

namespace PollyClientWebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PollyController(ILogger<PollyController> logger, IHttpClientFactory httpClientFactory, ResiliencePipelineProvider<string> resiliencePipelineProvider) : ControllerBase
{
    [HttpGet("slow-response")]
    public async Task<IActionResult> GetSlowResponse()
    {
        var client = httpClientFactory.CreateClient("PollyServerWebApi");
        // The following code is the normal way to call an API.
        //var response = await client.GetAsync("api/slow-response");
        //var content = await response.Content.ReadAsStringAsync();

        // The following code uses HttpClient timeout to define a timeout policy.
        //client.Timeout = TimeSpan.FromSeconds(5);
        //try
        //{
        //    var response = await client.GetAsync("api/slow-response");
        //    var content = await response.Content.ReadAsStringAsync();
        //    return Ok(content);
        //}
        //catch (Exception e)
        //{
        //    logger.LogError($"{e.GetType()} {e.Message}");
        //    return Problem(e.Message);
        //}



        // The following code uses a CancellationToken to define a timeout policy.
        //try
        //{
        //    var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        //    var response = await client.GetAsync("api/slow-response", cts.Token);
        //    var content = await response.Content.ReadAsStringAsync();
        //    return Ok(content);
        //}
        //catch (Exception e)
        //{
        //    logger.LogError($"{e.GetType()} {e.Message}");
        //    return Problem(e.Message);
        //}

        // The following code uses Polly to define a timeout policy.
        //var pipeline = new ResiliencePipelineBuilder().AddTimeout(TimeSpan.FromSeconds(5)).Build();
        //try
        //{
        //    var response = await pipeline.ExecuteAsync(async cancellationToken =>
        //        await client.GetAsync("api/slow-response", cancellationToken));
        //    var content = await response.Content.ReadAsStringAsync();
        //    return Ok(content);
        //}
        //catch (Exception e)
        //{
        //    logger.LogError($"{e.GetType()} {e.Message}");
        //    return Problem(e.Message);
        //}

        // The following code uses DI to get a timeout policy.
        try
        {
            var pipeline = resiliencePipelineProvider.GetPipeline("timeout-5s-pipeline");
            var response = await pipeline.ExecuteAsync(async cancellationToken =>
                               await client.GetAsync("api/slow-response", cancellationToken));
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }
        catch (Exception e)
        {
            logger.LogError($"{e.GetType()} {e.Message}");
            return Problem(e.Message);
        }
    }

    [HttpGet("rate-limit")]
    public async Task<IActionResult> GetNormalResponseWithRateLimiting()
    {
        var client = httpClientFactory.CreateClient("PollyServerWebApi");
        try
        {
            var pipeline = resiliencePipelineProvider.GetPipeline("rate-limit-5-requests-in-3-seconds");
            var response = await pipeline.ExecuteAsync(async cancellationToken =>
                await client.GetAsync("api/normal-response", cancellationToken));
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }
        catch (Exception e)
        {
            logger.LogError($"{e.GetType()} {e.Message}");
            return Problem(e.Message);
        }
    }

    [HttpGet("circuit-breaker")]
    public async Task<IActionResult> GetRandomFailureResponseWithCircuitBreaker()
    {
        var client = httpClientFactory.CreateClient("PollyServerWebApi");
        try
        {
            var pipeline = resiliencePipelineProvider.GetPipeline("circuit-breaker-5-seconds");
            var response = await pipeline.ExecuteAsync(async cancellationToken =>
                {
                    var result = await client.GetAsync("api/random-failure-response", cancellationToken);
                    result.EnsureSuccessStatusCode();
                    return result;
                });
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }
        catch (Exception e)
        {
            logger.LogError($"{e.GetType()} {e.Message}");
            return Problem(e.Message);
        }
    }
}

using Grpc.Core;

namespace GrpcDemo.Services;

public class RandomNumbersService : RandomNumbers.RandomNumbersBase
{
    private readonly ILogger<RandomNumbersService> _logger;

    public RandomNumbersService(ILogger<RandomNumbersService> logger)
    {
        _logger = logger;
    }

    public override async Task GetRandomNumbers(GetRandomNumbersRequest request,
        IServerStreamWriter<GetRandomNumbersResponse> responseStream, ServerCallContext context)
    {
        var random = new Random();
        for (var i = 0; i < request.Count; i++)
        {
            await responseStream.WriteAsync(new GetRandomNumbersResponse
            {
                Number = random.Next(request.Min, request.Max)
            });
            await Task.Delay(1000);
        }
    }

    // The following code continues to stream random numbers until the client cancels the request.
    //public override async Task GetRandomNumbers(GetRandomNumbersRequest request,
    //    IServerStreamWriter<GetRandomNumbersResponse> responseStream, ServerCallContext context)
    //{
    //    var random = new Random();
    //    while (!context.CancellationToken.IsCancellationRequested)
    //    {
    //        await responseStream.WriteAsync(new GetRandomNumbersResponse
    //        {
    //            Number = random.Next(request.Min, request.Max)
    //        });
    //        await Task.Delay(1000, context.CancellationToken);
    //    }
    //}
}

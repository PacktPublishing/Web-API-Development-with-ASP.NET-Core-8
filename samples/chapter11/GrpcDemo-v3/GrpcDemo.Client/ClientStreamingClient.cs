using Grpc.Net.Client;

namespace GrpcDemo.Client;
internal class ClientStreamingClient
{
    public async Task SendRandomNumbers()
    {
        using var channel = GrpcChannel.ForAddress("https://localhost:7179");
        var client = new RandomNumbers.RandomNumbersClient(channel);

        // Create a streaming request
        using var clientStreamingCall = client.SendRandomNumbers();
        var random = new Random();
        for (var i = 0; i < 20; i++)
        {
            await clientStreamingCall.RequestStream.WriteAsync(new SendRandomNumbersRequest
            {
                Number = random.Next(1, 100)
            });
            await Task.Delay(1000);
        }
        await clientStreamingCall.RequestStream.CompleteAsync();

        // Get the response
        var response = await clientStreamingCall;
        Console.WriteLine($"Count: {response.Count}, Sum: {response.Sum}");
        Console.ReadKey();
    }
}

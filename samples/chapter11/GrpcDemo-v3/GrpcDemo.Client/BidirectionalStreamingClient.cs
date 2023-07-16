using Grpc.Core;
using Grpc.Net.Client;

namespace GrpcDemo.Client;
internal class BidirectionalStreamingClient
{
    public async Task SendMessage()
    {
        using var channel = GrpcChannel.ForAddress("https://localhost:7179");
        var client = new Chat.ChatClient(channel);

        // Create a streaming request
        using var streamingCall = client.SendMessage();
        Console.WriteLine("Starting background task to receive messages...");
        var responseReaderTask = Task.Run(async () =>
        {
            await foreach (var response in streamingCall.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine(response.Message);
            }
        });

        Console.WriteLine("Starting to send messages...");
        Console.WriteLine("Type a message then press enter.");
        while (true)
        {
            var message = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(message))
            {
                break;
            }
            await streamingCall.RequestStream.WriteAsync(new ChatMessage
            {
                Message = message
            });
        }
        Console.WriteLine("Disconnecting...");
        await streamingCall.RequestStream.CompleteAsync();
        await responseReaderTask;
    }
}

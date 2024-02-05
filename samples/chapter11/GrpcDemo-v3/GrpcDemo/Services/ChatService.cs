using Grpc.Core;

namespace GrpcDemo.Services;
public class ChatService(ILogger<ChatService> logger) : Chat.ChatBase
{
    public override async Task SendMessage(IAsyncStreamReader<ChatMessage> requestStream, IServerStreamWriter<ChatMessage> responseStream, ServerCallContext context)
    {
        await foreach (var request in requestStream.ReadAllAsync())
        {
            logger.LogInformation($"Received: {request.Message}");
            await responseStream.WriteAsync(new ChatMessage
            {
                Message = $"You said: {request.Message.ToUpper()}"
            });
        }
    }
}
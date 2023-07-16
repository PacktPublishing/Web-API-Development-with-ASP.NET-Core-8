using Grpc.Core;

namespace GrpcDemo.Services;
public class ChatService : Chat.ChatBase
{
    private readonly ILogger<ChatService> _logger;

    public ChatService(ILogger<ChatService> logger)
    {
        _logger = logger;
    }

    public override async Task SendMessage(IAsyncStreamReader<ChatMessage> requestStream, IServerStreamWriter<ChatMessage> responseStream, ServerCallContext context)
    {
        await foreach (var request in requestStream.ReadAllAsync())
        {
            _logger.LogInformation($"Received: {request.Message}");
            await responseStream.WriteAsync(new ChatMessage
            {
                Message = $"You said: {request.Message.ToUpper()}"
            });
        }
    }
}
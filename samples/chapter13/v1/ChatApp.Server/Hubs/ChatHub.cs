
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Server.Hubs;

public class ChatHub : Hub
{
    public Task SendMessage(string username, string message)
    {
        return Clients.All.SendAsync("ReceiveMessage", username, message);
    }
}



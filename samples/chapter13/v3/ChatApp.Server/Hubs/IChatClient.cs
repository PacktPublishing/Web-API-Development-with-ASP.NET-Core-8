namespace ChatApp.Server.Hubs;

public interface IChatClient
{
    Task ReceiveMessage(string user, string message);
    Task UserConnected(string user);
    Task UserDisconnected(string user);
}

using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Server.Services;

public class NameUserIdProvider : IUserIdProvider
{
    public string GetUserId(HubConnectionContext connection)
    {
        return connection.User?.Identity?.Name ?? string.Empty;
    }
}
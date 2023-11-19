namespace ChatApp.Server.Models;

public class SendToAllMessageModel
{
    public string FromUser { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class SendToUserMessageModel
{
    public string FromUser { get; set; } = string.Empty;
    public string ToUser { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class SendToGroupMessageModel
{
    public string FromUser { get; set; } = string.Empty;
    public string GroupName { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
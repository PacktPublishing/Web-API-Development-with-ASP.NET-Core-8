using ChatApp.Server.Hubs;
using ChatApp.Server.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Server.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ChatController(IHubContext<ChatHub, IChatClient> hubContext) : ControllerBase
{
    [HttpPost("/all")]
    public async Task<IActionResult> SendToAllMessage([FromBody] SendToAllMessageModel model)
    {
        if (ModelState.IsValid)
        {
            await hubContext.Clients.All.ReceiveMessage(model.FromUser, model.Message);
            return Ok();
        }
        return BadRequest(ModelState);
    }

    [HttpPost("/user")]
    public async Task<IActionResult> SendToUserMessage([FromBody] SendToUserMessageModel model)
    {
        if (ModelState.IsValid)
        {
            await hubContext.Clients.User(model.ToUser).ReceiveMessage(model.FromUser, model.Message);
            return Ok();
        }
        return BadRequest(ModelState);
    }

    [HttpPost("/group")]
    public async Task<IActionResult> SendToGroupMessage([FromBody] SendToGroupMessageModel model)
    {
        if (ModelState.IsValid)
        {
            await hubContext.Clients.Group(model.GroupName).ReceiveMessage(model.FromUser, model.Message);
            return Ok();
        }
        return BadRequest(ModelState);
    }
}

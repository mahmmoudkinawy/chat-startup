using API.Hubs;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers;

[ApiController]
[Route("api/chat")]
public class ChatController : ControllerBase
{
    private readonly IHubContext<ChatHub> _hubContext;

    public ChatController(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext ??
            throw new ArgumentNullException(nameof(hubContext));
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(
        [FromBody] MessageDto message)
    {
        await _hubContext.Clients.All.SendAsync(
            "ReceiveMessage", message.User, message.Content);

        return Ok(); //Maybe will be changed later.
    }
}

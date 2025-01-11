using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Qyrenx.Business.Services.HubsServices;

namespace Qyrenx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationController(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost("send-notification")]
        public async Task<IActionResult> SendNotification([FromBody] string message)
        {
            // Sends notification to all connected clients
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", message);
            return Ok("send successfull");
        }
    }
}

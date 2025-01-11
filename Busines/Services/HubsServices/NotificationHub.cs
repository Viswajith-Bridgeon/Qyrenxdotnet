using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Business.Services.HubsServices
{
    public class NotificationHub : Hub
    {
        private static readonly Dictionary<string, string> _userConnections = new Dictionary<string, string>();

        public override Task OnConnectedAsync()
        {
            var userId = Context.GetHttpContext().Request.Query["userId"];
            if (!string.IsNullOrEmpty(userId))
            {
                _userConnections[userId] = Context.ConnectionId;
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var userId = _userConnections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
            if (!string.IsNullOrEmpty(userId))
            {
                _userConnections.Remove(userId);
            }
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendNotificationToUser(string userId, string message)
        {
            if (_userConnections.ContainsKey(userId))
            {
                var connectionId = _userConnections[userId];
                await Clients.Client(connectionId).SendAsync("ReceiveNotification", message);
            }
        }
    }

}

using Microsoft.AspNetCore.SignalR;

namespace GymPlanner.WebUI.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Send(string message, string userName)
        {
            await Clients.All.SendAsync("Send", message, userName);
        }
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}

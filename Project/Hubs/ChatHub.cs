using Microsoft.AspNetCore.SignalR;

namespace GymPlanner.WebUI.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync("Send", message);
        }
    }
}

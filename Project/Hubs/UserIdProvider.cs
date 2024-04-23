using Microsoft.AspNetCore.SignalR;

namespace GymPlanner.WebUI.Hubs
{
    public class UserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            return connection.User?.Identity?.Name;
        }
    }
}

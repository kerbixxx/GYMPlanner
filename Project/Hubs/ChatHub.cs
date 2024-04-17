using GymPlanner.Application.Interfaces.Repositories.Chat;
using GymPlanner.Domain.Entities.Chat;
using Microsoft.AspNetCore.SignalR;

namespace GymPlanner.WebUI.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageRepository _chatRepository;

        public ChatHub(IMessageRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }
        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync("Send", message);
        }
        public async Task SendMessage(string senderId, string receiverId, string dialogId, string message)
        {
            var newMessage = new Message
            {
                UserIdFrom = int.Parse(senderId),
                UserIdTo = int.Parse(receiverId),
                Content = message,
                Created = DateTime.Now,
                DialogId = int.Parse(dialogId)
            };
            await _chatRepository.AddAsync(newMessage);

            await Clients.User(receiverId).SendAsync("ReceiveMessage", senderId, message);
            await Clients.User(senderId).SendAsync("ReceiveMessage", senderId, message);
        }
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}

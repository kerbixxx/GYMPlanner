using GymPlanner.Application.Interfaces.Repositories;
using GymPlanner.Application.Interfaces.Repositories.Chat;
using GymPlanner.Domain.Entities.Chat;
using GymPlanner.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace GymPlanner.WebUI.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IMessageRepository _chatRepository;
        public ChatHub(IMessageRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            var claims = Context.User;
            await base.OnConnectedAsync();
        }
        public async Task SendMessage(int senderId, int receiverId, string receiverName, int dialogId, string message)
        {
            var newMessage = new Message
            {
                UserIdFrom = senderId,
                UserIdTo = receiverId,
                Content = message,
                Created = DateTime.Now,
                DialogId = dialogId
            };
            var senderName = Context.UserIdentifier;
            await _chatRepository.AddAsync(newMessage);
            await Clients.User(receiverName).SendAsync("ReceiveMessage", senderName, message);
            await Clients.User(senderName).SendAsync("ReceiveMessage", senderName, message);
        }
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}

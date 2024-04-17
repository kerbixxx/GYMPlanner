using GymPlanner.Application.Interfaces.Repositories;
using GymPlanner.Application.Interfaces.Repositories.Chat;
using GymPlanner.Domain.Entities.Chat;
using GymPlanner.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace GymPlanner.WebUI.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IMessageRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        public ChatHub(IMessageRepository chatRepository, IUserRepository userRepository)
        {
            _chatRepository = chatRepository;
            _userRepository = userRepository;
        }

        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            var claims = Context.User;
            await base.OnConnectedAsync();
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
            var userName = await _userRepository.GetAsync(int.Parse(receiverId));
            await Clients.User(userName.Email).SendAsync("ReceiveMessage", senderId, message);
            await Clients.User(Context.UserIdentifier).SendAsync("ReceiveMessage", senderId, message);
        }
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}

using GymPlanner.Domain.Entities.Chat;

namespace GymPlanner.Application.Interfaces.Repositories.Chat
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<List<Message>> GetMessagesFromDialogAsync(int currentUserId, int userId);
    }
}

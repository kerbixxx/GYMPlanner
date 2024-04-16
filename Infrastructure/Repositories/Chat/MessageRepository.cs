using GymPlanner.Application.Interfaces.Repositories.Chat;
using GymPlanner.Domain.Entities.Chat;
using GymPlanner.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Infrastructure.Repositories.Chat
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        private readonly PlanDbContext _db;
        public MessageRepository(PlanDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Message>> GetMessagesFromDialogAsync(int currentUserId, int userId)
        {
            return await _db.Messages.Where(m => (m.UserIdFrom == currentUserId && m.UserIdTo == userId) ||
                            (m.UserIdFrom == userId && m.UserIdTo == currentUserId))
                .OrderBy(m => m.Created)
                .ToListAsync();
        }
    }
}

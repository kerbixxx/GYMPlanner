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
        public MessageRepository(PlanDbContext db) : base(db)
        {
        }

        public async Task<List<Message>> GetMessagesFromDialogAsync(int dialogId)
        {
            return await _db.Messages.Where(m=>m.DialogId==dialogId)
                .Include(m=>m.Dialog)
                .Select(m=>new Message()
                {
                    DialogId = dialogId,
                    Content = m.Content,
                    Created = m.Created,
                    Id = m.Id,
                    UserIdFrom = m.UserIdFrom,
                    UserIdTo = m.UserIdTo,
                    Dialog = m.Dialog,
                })
                .OrderBy(m => m.Created)
                .ToListAsync();
        }
    }
}

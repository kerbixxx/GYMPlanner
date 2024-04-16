using GymPlanner.Application.Interfaces.Repositories.Chat;
using GymPlanner.Domain.Entities.Chat;
using GymPlanner.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Infrastructure.Repositories.Chat
{
    public class ChatRepository : Repository<Message>, IChatRepository 
    {
        private readonly PlanDbContext _db;
        public ChatRepository(PlanDbContext db) : base(db)
        {
            _db = db;
        }
    }
}

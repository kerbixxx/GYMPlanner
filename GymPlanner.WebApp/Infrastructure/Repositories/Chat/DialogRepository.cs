using GymPlanner.Application.Interfaces.Repositories.Chat;
using GymPlanner.Domain.Entities.Chat;
using GymPlanner.Domain.Entities.Identity;
using GymPlanner.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Infrastructure.Repositories.Chat
{
    public class DialogRepository : Repository<Dialog>, IDialogRepository
    {
        public DialogRepository(PlanDbContext db) : base(db)
        {   
        }

        public async Task<Dialog> GetDialogBetweenUsersAsync(int userId, int otherUserId)
        {
            return await _db.Dialogs
                .Include(u=>u.OtherUser)
                .FirstOrDefaultAsync(u => (u.UserId == userId && u.OtherUserId == otherUserId)||(u.UserId==otherUserId && u.OtherUserId == userId));
        }

        public async Task<List<Dialog>> GetUserDialogsAsync(int id)
        {
            return await _db.Dialogs
                .Include(d=>d.User)
                .Include(d=>d.OtherUser)
                .Where(d => d.UserId == id || d.OtherUserId == id).ToListAsync();
        }

        public override async Task<Dialog> GetAsync(int id)
        {
            return await _db.Dialogs
                .Include(d => d.User)
                .Include(d => d.OtherUser)
                .Include(d => d.Messages)
                .Select(d => new Dialog()
                {
                    UserId = d.UserId,
                    OtherUserId = d.OtherUserId,
                    Messages = d.Messages,
                    Id = d.Id,
                    User = new User { Email = d.User.Email },
                    OtherUser = new User { Email = d.OtherUser.Email}
                })
                .FirstOrDefaultAsync(d=>d.Id == id);
        }
    }
}

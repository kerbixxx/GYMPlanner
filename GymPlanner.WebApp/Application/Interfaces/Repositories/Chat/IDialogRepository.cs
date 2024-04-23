using GymPlanner.Domain.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Application.Interfaces.Repositories.Chat
{
    public interface IDialogRepository : IRepository<Dialog>
    {
        Task<List<Dialog>> GetUserDialogsAsync(int id);
        Task<Dialog> GetDialogBetweenUsersAsync(int userId, int otherUserId);
    }
}

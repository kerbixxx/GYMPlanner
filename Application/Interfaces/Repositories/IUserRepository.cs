using GymPlanner.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Application.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> FindByNameAsync(string name);
    }
}

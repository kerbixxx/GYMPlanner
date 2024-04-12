using GymPlanner.Application.Interfaces.Repositories;
using GymPlanner.Domain.Entities.Identity;
using GymPlanner.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly PlanDbContext _db;
        public UserRepository(PlanDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<User> FindByNameAsync(string name)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == name);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _db.Users.
                Include(u=>u.Role).ToListAsync();
        }
    }
}

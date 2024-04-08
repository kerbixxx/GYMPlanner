using GymPlanner.Application.Interfaces.Repositories;
using GymPlanner.Domain.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GymPlanner.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        DbSet<T> _dbSet;
        public virtual T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }
    }
}

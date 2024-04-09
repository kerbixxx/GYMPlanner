using GymPlanner.Application.Interfaces.Repositories;
using GymPlanner.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GymPlanner.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        DbSet<T> _dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this._dbSet = _db.Set<T>();
        }

        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async virtual Task<T> Get(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}

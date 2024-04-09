namespace GymPlanner.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task Add(T entity);
        void Remove(T entity);
        void Update(T entity);
        Task Save();
    }
}

namespace GymPlanner.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T Get(int id);
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
        void Save();
    }
}

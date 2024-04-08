using Microsoft.Identity.Client;

namespace GymPlanner.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T Get(int id);
    }
}

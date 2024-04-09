using GymPlanner.Application.Interfaces.Repositories;
using GymPlanner.Infrastructure.Contexts;
using GymPlanner.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GymPlanner.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            var assembly = Assembly.GetAssembly(typeof(ApplicationDbContext));
            var repositoryTypes = assembly.GetTypes()
                .Where(type => !type.IsAbstract && !type.IsInterface && type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IRepository<>)));

            // filter out RepositoryBase<>
            var nonBaseRepos = repositoryTypes.Where(t => t != typeof(Repository<>));

            foreach (var repositoryType in nonBaseRepos)
            {
                var interfaces = repositoryType.GetInterfaces()
                    .Where(@interface => @interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(IRepository<>))
                    .ToList();

                if (interfaces.Count != 1)
                {
                    throw new InvalidOperationException($"Repository '{repositoryType.Name}' must implement only one interface that implements IRepositoryBase<T>.");
                }

                services.AddScoped(interfaces[0], repositoryType);
            }

            //services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}

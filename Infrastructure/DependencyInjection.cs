    using GymPlanner.Application.Interfaces.Repositories;
    using GymPlanner.Infrastructure.Contexts;
    using GymPlanner.Infrastructure.Repositories;
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

    namespace GymPlanner.Infrastructure
    {
        public static class DependencyInjection
        {
            public static IServiceCollection AddRepositories(this IServiceCollection services)
            {
                services.AddScoped<IPlanRepository, PlanRepository>();
                services.AddScoped<IPlanExcersiseFrequencyRepository, PlanExcersiseFrequencyRepository>();
                return services;
            }
        }
    }

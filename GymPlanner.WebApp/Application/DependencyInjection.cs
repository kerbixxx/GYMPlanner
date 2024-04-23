using GymPlanner.Application.Interfaces.Services;
using GymPlanner.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GymPlanner.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(assembly);
            });

            return services;
        }
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddTransient<IPlanService, PlanService>();
            services.AddTransient<IRatingService, RatingService>();
            return services;
        }
    }
}

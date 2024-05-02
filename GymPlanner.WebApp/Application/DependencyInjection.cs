using GymPlanner.Application.Interfaces.Services;
using GymPlanner.Application.Mappings;
using GymPlanner.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GymPlanner.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(PlanMappingProfile));

            return services;
        }
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IPlanService, PlanService>();
            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<IExerciseService, ExerciseService>();
            services.AddScoped<IFrequencyService, FrequencyService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            return services;
        }
    }
}

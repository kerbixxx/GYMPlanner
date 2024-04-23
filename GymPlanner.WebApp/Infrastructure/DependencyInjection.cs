using GymPlanner.Application.Interfaces.Repositories;
using GymPlanner.Application.Interfaces.Repositories.Chat;
using GymPlanner.Application.Interfaces.Repositories.Plan;
using GymPlanner.Application.Interfaces.Services;
using GymPlanner.Infrastructure.Repositories;
using GymPlanner.Infrastructure.Repositories.Chat;
using GymPlanner.Infrastructure.Repositories.Plan;
using GymPlanner.Application.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using GymPlanner.Infrastructure.Services;

namespace GymPlanner.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<IPlanExerciseFrequencyRepository, PlanExcersiseFrequencyRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IExerciseRepository, ExerciseRepository>();
            services.AddScoped<IFrequencyRepository, FrequencyRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IDialogRepository, DialogRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            return services;
        }
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IRabbitMQProducer, RabbitMQProducer>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddTransient<IPlanService, PlanService>();
            return services;
        }
    }
}

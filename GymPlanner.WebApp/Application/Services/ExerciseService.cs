using GymPlanner.Application.Configurations;
using GymPlanner.Application.Interfaces.Repositories.Plan;
using GymPlanner.Application.Interfaces.Services;
using GymPlanner.Application.Models.Plan;
using GymPlanner.Domain.Entities.Plans;
using Microsoft.Extensions.Options;

namespace GymPlanner.Application.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IPlanExerciseFrequencyRepository _pefRepository;
        private readonly DefaultNamesOptions _options;
        public ExerciseService(IExerciseRepository exerciseRepository, IPlanExerciseFrequencyRepository pefRepository, IOptions<DefaultNamesOptions> options)
        {
            _exerciseRepository = exerciseRepository;
            _pefRepository = pefRepository;
            _options = options.Value;
        }

        public async Task<Exercise> AddExerciseToPlan(ExerciseDto dto)
        {
            Exercise exercise = new() { Name = dto.Name };
            await _exerciseRepository.AddAsync(exercise);
            var pefList = new List<PlanExerciseFrequency>();
            var plan = await _pefRepository.GetByPlanId(dto.PlanId);
            foreach(var freq in plan.Select(p => p.FrequencyId).Distinct())
            {
                pefList.Add(new PlanExerciseFrequency()
                {
                    FrequencyId = freq,
                    ExerciseId = exercise.Id,
                    PlanId = dto.PlanId,
                    Description = _options.DEFAULT_DESCRIPTION
                });
            }
            foreach (var pef in pefList)
            {
                await _pefRepository.AddAsync(pef);
            }
            return exercise;
        }

        public async Task DeleteExerciseFromPlan(int id)
        {
            var exercise = await _exerciseRepository.GetAsync(id);
            await _exerciseRepository.RemoveAsync(exercise);
        }

        public async Task UpdateAsync(Exercise obj)
        {
            await _exerciseRepository.UpdateAsync(obj);
        }
    }
}

using GymPlanner.Application.Configurations;
using GymPlanner.Application.Interfaces.Repositories.Plan;
using GymPlanner.Application.Interfaces.Services;
using GymPlanner.Application.Models.Plan;
using GymPlanner.Domain.Entities.Plans;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Application.Services
{
    public class FrequencyService : IFrequencyService
    {
        private readonly IFrequencyRepository _frequencyRepository;
        private readonly IPlanExerciseFrequencyRepository _pefRepository;
        private readonly DefaultNamesOptions _options;
        public FrequencyService(IFrequencyRepository frequencyRepository, IPlanExerciseFrequencyRepository pefRepository, IOptions<DefaultNamesOptions> options)
        {
            _frequencyRepository = frequencyRepository;
            _pefRepository = pefRepository;
            _options = options.Value;
        }

        public async Task<Frequency> AddFrequencyToPlan(FrequencyDto dto)
        {
            Frequency frequency = new() { Name = dto.Name };
            await _frequencyRepository.AddAsync(frequency);
            var pefList = new List<PlanExerciseFrequency>();
            var plan = await _pefRepository.GetByPlanId(dto.PlanId);
            foreach (var exercise in plan.Select(p => p.ExerciseId).Distinct())
            {
                pefList.Add(new PlanExerciseFrequency()
                {
                    FrequencyId = frequency.Id,
                    ExerciseId = exercise,
                    PlanId = dto.PlanId,
                    Description = _options.DEFAULT_DESCRIPTION
                });
            }
            foreach (var pef in pefList)
            {
                await _pefRepository.AddAsync(pef);
            }
            return frequency;
        }

        public async Task DeleteFrequencyFromPlan(int id)
        {
            var frequency = await _frequencyRepository.GetAsync(id);
            await _frequencyRepository.RemoveAsync(frequency);
        }

        public async Task UpdateAsync(Frequency obj)
        {
            await _frequencyRepository.UpdateAsync(obj);
        }
    }
}

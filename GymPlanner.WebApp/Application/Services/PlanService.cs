using AutoMapper;
using GymPlanner.Application.Configurations;
using GymPlanner.Application.Interfaces.Repositories.Plan;
using GymPlanner.Application.Interfaces.Services;
using GymPlanner.Application.Models.Plan;
using GymPlanner.Domain.Entities.Plans;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Application.Services
{
    public class PlanService : IPlanService
    {
        private readonly DefaultNamesOptions _options;
        private readonly IPlanRepository _planRepo;
        private readonly IPlanExerciseFrequencyRepository _pefRepo;
        private readonly IFrequencyService _frequencyService;
        private readonly IRatingService _ratingService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IExerciseService _exerciseService;
        private readonly IMapper _mapper;
        public PlanService(IPlanRepository planRepo,
                           IPlanExerciseFrequencyRepository pefRepo,
                           IFrequencyService frequencyService,
                           IRatingService ratingService,
                           ISubscriptionService subscriptionService,
                           IExerciseService exerciseService,
                           IOptions<DefaultNamesOptions> options,
                           IMapper mapper)
        {
            _planRepo = planRepo;
            _pefRepo = pefRepo;
            _frequencyService = frequencyService;
            _ratingService = ratingService;
            _subscriptionService = subscriptionService;
            _exerciseService = exerciseService;
            _options = options.Value;
            _mapper = mapper;
        }
        public async Task AddPlanAsync(Plan plan)
        {
            plan.CreatedAt = DateTime.Now;
            await _planRepo.AddAsync(plan);
            var exercise = await _exerciseService.AddExerciseToPlan(new ExerciseDto() { Name = _options.DEFAULT_EXERCISE_NAME,PlanId = plan.Id});
            var frequency = await _frequencyService.AddFrequencyToPlan(new FrequencyDto() { Name = _options.DEFAULT_FREQUENCY_NAME, PlanId = plan.Id });
            PlanExerciseFrequency pef = new()
            {
                PlanId = plan.Id,
                FrequencyId = frequency.Id,
                ExerciseId = exercise.Id,
                Description = _options.DEFAULT_DESCRIPTION
            };

            await _pefRepo.AddAsync(pef);
        }

        public async Task UpdatePlanAsync(Plan plan)
        {
            await _planRepo.UpdateAsync(plan);
        }

        public async Task<List<Plan>> GetAllPlansAsync()
        {
            return await _planRepo.GetAll();
        }

        public async Task<List<GetPlansOnIndexDto>> GetFilteredPlans(string? tag, string? sortBy, string? sortOrder)
        {
            var plans = await _planRepo.GetAll();
            List<Plan> filteredPlans = new();
            if(tag != null)
            {
                filteredPlans = plans.Where(t => t.Tags.Any(t=>t.Contains(tag))).ToList();
            }
            else
            {
                filteredPlans = plans;
            }
            List<GetPlansOnIndexDto> plansDto = new();
            foreach(var plan in filteredPlans)
            {
                var planDto = _mapper.Map<GetPlansOnIndexDto>(plan);
                planDto.AverageRating = await _ratingService.GetAverageRatingForPlan(plan.Id);
                plansDto.Add(planDto);
            }
            if (sortBy != null)
            {
                if (sortBy == "Rating")
                {
                    plansDto = sortOrder == "asc" ? plansDto.OrderBy(t => t.AverageRating).ToList() : plansDto.OrderByDescending(t => t.AverageRating).ToList();
                }
                if (sortBy == "CreatedDate")
                {
                    plansDto = sortOrder == "asc" ? plansDto.OrderBy(t => t.CreatedAt).ToList() : plansDto.OrderByDescending(t => t.CreatedAt).ToList();
                }
                //Сортировка по комментариям
            }
            return plansDto;
        }

        public async Task UpdatePlanAsync(PlanEditDto planDto)
        {
            var plan = _mapper.Map<Plan>(planDto);
            var trimmedTags = plan.Tags.Select(tag => tag.Trim()).ToArray();
            plan.Tags = trimmedTags;
            await _planRepo.UpdateAsync(plan);
            foreach (var pef in planDto.ExerciseFrequencies)
            {
                await _pefRepo.UpdateAsync(new PlanExerciseFrequency() { Id = pef.Id, ExerciseId = pef.ExerciseId, PlanId = planDto.PlanId, FrequencyId = pef.FrequencyId, Description = pef.Description });
            }
            foreach (var excersise in planDto.Exercises)
            {
                await _exerciseService.UpdateAsync(excersise);
            }
            foreach (var frequency in planDto.Frequencies)
            {
                await _frequencyService.UpdateAsync(frequency);
            }
            await _subscriptionService.NotifyAllSubscribers(plan.Id);
        }
        public async Task DeletePlanAsync(int id)
        {
            var plan = await _planRepo.GetAsync(id);
            await _planRepo.RemoveAsync(plan);
        }

        public async Task<PlanEditDto> GetPlanEditDtoAsync(int id)
        {
            var plan = await _planRepo.GetAsync(id);
            await CheckIfEmpty(plan);
            var planDto = _mapper.Map<PlanEditDto>(plan);
            return planDto;
        }

        private async Task CheckIfEmpty(Plan plan)
        {
            if (plan.planExersiseFrequencies.Count() == 0)
            {
                var exercise = await _exerciseService.AddExerciseToPlan(new ExerciseDto() { Name = _options.DEFAULT_EXERCISE_NAME, PlanId = plan.Id });
                var frequency = await _frequencyService.AddFrequencyToPlan(new FrequencyDto() { Name = _options.DEFAULT_FREQUENCY_NAME, PlanId = plan.Id });
                PlanExerciseFrequency pef = new()
                {
                    PlanId = plan.Id,
                    FrequencyId = frequency.Id,
                    ExerciseId = exercise.Id,
                    Description = _options.DEFAULT_DESCRIPTION
                };

                await _pefRepo.AddAsync(pef);
            }
        }

        public async Task<PlanDetailsDto> GetPlanDetailsDtoAsync(int id, int userId)
        {
            var plan = await _planRepo.GetAsync(id);
            var planDto = _mapper.Map<PlanDetailsDto>(plan);
            planDto.IsSubscribed = _subscriptionService.IsUserSubbedToPlan(userId, plan.Id);
            return planDto;
        }
    }
}

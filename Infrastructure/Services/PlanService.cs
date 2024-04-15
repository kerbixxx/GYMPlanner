﻿using GymPlanner.Application.Interfaces.Repositories.Plan;
using GymPlanner.Application.Interfaces.Services;
using GymPlanner.Application.Models.Plan;
using GymPlanner.Domain.Entities.Plans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Infrastructure.Services
{
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository _planRepo;
        private readonly IPlanExerciseFrequencyRepository _pefRepo;
        private readonly IExerciseRepository _exerciseRepo;
        private readonly IFrequencyRepository _frequencyRepo;
        public PlanService(IPlanRepository planRepo, IPlanExerciseFrequencyRepository pefRepo, IExerciseRepository exerciseRepository, IFrequencyRepository frequencyRepository)
        {
            _planRepo = planRepo;
            _pefRepo = pefRepo;
            _exerciseRepo = exerciseRepository;
            _frequencyRepo = frequencyRepository;
        }

        public async Task AddPlanAsync(Plan plan)
        {
            await _planRepo.AddAsync(plan);
            Exercise exercise = new()
            {
                Name = "Упражнение 1"
            };
            Frequency frequency = new()
            {
                Name = "Частота 1"
            };
            await _exerciseRepo.AddAsync(exercise);
            await _frequencyRepo.AddAsync(frequency);
            PlanExerciseFrequency pef = new()
            {
                PlanId = plan.Id,
                FrequencyId = frequency.Id,
                ExerciseId = exercise.Id,
                Description = "0"
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

        public async Task UpdatePlanAsync(PlanEditDto planDto)
        {
            var plan = new Plan()
            {
                Id = planDto.PlanId,
                Name = planDto.Name,
                UserId = planDto.UserId,
            };
            await _planRepo.UpdateAsync(plan);
            foreach (var pef in planDto.ExerciseFrequencies)
            {
                await _pefRepo.UpdateAsync(new PlanExerciseFrequency() { Id = pef.Id, ExerciseId = pef.ExerciseId, PlanId = planDto.PlanId, FrequencyId = pef.FrequencyId, Description = pef.Description });
            }
            foreach (var excersise in planDto.Exercises)
            {
                await _exerciseRepo.UpdateAsync(excersise);
            }
            foreach (var frequency in planDto.Frequencies)
            {
                await _frequencyRepo.UpdateAsync(frequency);
            }
        }

        public async Task AddExerciseToPlan(ExerciseDto dto)
        {
            var plan = await _planRepo.GetAsync(dto.PlanId);
            Exercise exercise = new() { Name = dto.Name };
            await _exerciseRepo.AddAsync(exercise);
            var pefList = new List<PlanExerciseFrequency>();
            foreach (var freq in plan.planExersiseFrequencies.Select(p => p.FrequencyId).Distinct())
            {
                pefList.Add(new PlanExerciseFrequency()
                {
                    FrequencyId = freq,
                    ExerciseId = exercise.Id,
                    PlanId = plan.Id,
                    Description = "0"
                });
            }
            foreach (var pef in pefList)
            {
                await _pefRepo.AddAsync(pef);
            }
        }

        public async Task AddFrequencyToPlan(FrequencyDto dto)
        {
            var plan = await _planRepo.GetAsync(dto.PlanId);
            Frequency frequency = new() { Name = dto.Name };
            await _frequencyRepo.AddAsync(frequency);
            var pefList = new List<PlanExerciseFrequency>();
            foreach (var exercise in plan.planExersiseFrequencies.Select(p => p.ExerciseId).Distinct())
            {
                pefList.Add(new PlanExerciseFrequency()
                {
                    FrequencyId = frequency.Id,
                    ExerciseId = exercise,
                    PlanId = plan.Id,
                    Description = "0"
                });
            }
            foreach (var pef in pefList)
            {
                await _pefRepo.AddAsync(pef);
            }
        }

        public async Task DeletePlanAsync(int id)
        {
            var plan = await _planRepo.GetAsync(id);
            await _planRepo.RemoveAsync(plan);
        }

        public async Task DeleteFrequencyFromPlan(int id)
        {
            var frequency = await _frequencyRepo.GetAsync(id);
            await _frequencyRepo.RemoveAsync(frequency);
        }

        public async Task DeleteExerciseFromPlan(int id)
        {
            var exercise = await _exerciseRepo.GetAsync(id);
            await _exerciseRepo.RemoveAsync(exercise);
        }

        public async Task<PlanEditDto> GetPlanEditDtoAsync(int id)
        {
            var plan = await _planRepo.GetAsync(id);
            await CheckIfEmpty(plan);
            var excfreqList = new List<ExerciseFrequencyDto>();
            foreach (var pef in plan.planExersiseFrequencies)
            {
                var excfreq = new ExerciseFrequencyDto()
                {
                    Description = pef.Description,
                    ExerciseId = pef.Exercise.Id,
                    FrequencyId = pef.FrequencyId,
                    Id = pef.Id
                };
                excfreqList.Add(excfreq);
            }
            var planDto = new PlanEditDto()
            {
                PlanId = plan.Id,
                ExerciseFrequencies = excfreqList,
                Name = plan.Name,
                Exercises = plan.planExersiseFrequencies.Select(pef => pef.Exercise).Distinct().ToList(),
                Frequencies = plan.planExersiseFrequencies.Select(pef => pef.Frequency).Distinct().ToList(),
                UserId = plan.UserId
            };
            return planDto;
        }

        private async Task CheckIfEmpty(Plan plan)
        {
            if (plan.planExersiseFrequencies.Count() == 0)
            {
                Exercise exercise = new()
                {
                    Name = "Упражнение 1"
                };
                Frequency frequency = new()
                {
                    Name = "Частота 1"
                };
                await _exerciseRepo.AddAsync(exercise);
                await _frequencyRepo.AddAsync(frequency);
                PlanExerciseFrequency pef = new()
                {
                    PlanId = plan.Id,
                    FrequencyId = frequency.Id,
                    ExerciseId = exercise.Id,
                    Description = "0"
                };
                await _pefRepo.AddAsync(pef);
            }
        }
    }
}
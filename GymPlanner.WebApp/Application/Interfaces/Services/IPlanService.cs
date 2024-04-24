using GymPlanner.Application.Models.Plan;
using GymPlanner.Domain.Entities.Plans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Application.Interfaces.Services
{
    public interface IPlanService
    {
        Task UpdatePlanAsync(Plan plan);
        Task<PlanEditDto> GetPlanEditDtoAsync(int id);
        Task<PlanDetailsDto> GetPlanDetailsDtoAsync(int id, int userId);
        Task<List<Plan>> GetAllPlansAsync();
        Task UpdatePlanAsync(PlanEditDto planDto);
        Task AddExerciseToPlan(ExerciseDto dto);
        Task AddFrequencyToPlan(FrequencyDto dto);
        Task AddPlanAsync(Plan plan);
        Task DeleteFrequencyFromPlan(int id);
        Task DeleteExerciseFromPlan(int id);
        Task DeletePlanAsync(int id);
        Task<List<GetPlansOnIndexDto>> GetFilteredPlans(string? tag, string? sortBy, string? sortOrder);
    }
}

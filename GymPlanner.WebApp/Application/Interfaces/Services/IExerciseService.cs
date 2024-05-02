using GymPlanner.Application.Interfaces.Repositories.Plan;
using GymPlanner.Application.Models.Plan;
using GymPlanner.Domain.Entities.Plans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Application.Interfaces.Services
{
    public interface IExerciseService
    {
        Task<int> AddExerciseToPlan(ExerciseDto dto);
        Task DeleteExerciseFromPlan(int id);
        Task UpdateAsync(Exercise obj);
    }
}

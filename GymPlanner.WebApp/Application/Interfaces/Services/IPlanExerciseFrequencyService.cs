using GymPlanner.Domain.Entities.Plans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Application.Interfaces.Services
{
    public interface IPlanExerciseFrequencyService
    {
        Task AddPlanExerciseFrequency(PlanExerciseFrequency pef);
    }
}

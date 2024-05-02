using GymPlanner.Application.Interfaces.Repositories.Plan;
using GymPlanner.Application.Interfaces.Services;
using GymPlanner.Domain.Entities.Plans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Application.Services
{
    public class PlanExerciseFrequencyService : IPlanExerciseFrequencyService
    {
        private readonly IPlanExerciseFrequencyRepository _pefRepository;
        public PlanExerciseFrequencyService(IPlanExerciseFrequencyRepository pefRepository)
        {
            _pefRepository = pefRepository;
        }

        public Task AddPlanExerciseFrequency(PlanExerciseFrequency pef)
        {
            throw new NotImplementedException();
        }
    }
}

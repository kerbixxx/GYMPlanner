using GymPlanner.Domain.Entities.Plans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Application.Models.Plan
{
    public class PlanEditDto
    {
        public int PlanId { get; set; }
        public string Name { get; set; }
        public List<ExerciseFrequencyDto> ExerciseFrequencies { get; set; }
    }

    public class ExerciseFrequencyDto
    {
        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; }
        public int FrequencyId { get; set; }
        public string FrequencyName { get; set; }
        public string Description { get; set; }
    }

}

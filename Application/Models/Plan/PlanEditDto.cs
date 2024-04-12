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
        public List<Exercise> Excersises { get; set; }
        public List<Frequency> Frequencies { get; set; }
    }

    public class ExerciseFrequencyDto
    {
        public int ExerciseId { get; set; }
        public int FrequencyId { get; set; }
        public string Description { get; set; }
    }
    public class ExerciseDto
    {
        public int PlanId { get; set; }
        public string Name { get; set; }
    }
}

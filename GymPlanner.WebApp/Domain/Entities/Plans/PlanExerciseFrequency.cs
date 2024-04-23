using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Domain.Entities.Plans
{
    public class PlanExerciseFrequency : BaseEntity
    {
        public int PlanId { get; set; }
        public Plan? Plan { get; set; }

        public int FrequencyId { get; set; }
        public Frequency? Frequency { get; set; }

        public int ExerciseId { get; set; }
        public Exercise? Exercise { get; set; }

        public string Description { get; set; }
    }
}

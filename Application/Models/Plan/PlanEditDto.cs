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
        public List<Excersise>? Excersises { get; set; }
        public List<Frequency>? Frequencies { get; set; }
        public List<PlanExcersiseFrequency>? PlanExcersiseFrequencies { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Application.Models.Plan
{
    public class PlanDetailsViewModel
    {
        public PlanEditDto Plan { get; set; }
        public double? AverageRating { get; set; }
        public int? UserRating { get; set; }
    }
}

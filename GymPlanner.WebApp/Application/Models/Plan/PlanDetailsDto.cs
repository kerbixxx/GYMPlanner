using GymPlanner.Domain.Entities.Plans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Application.Models.Plan
{
    public class PlanDetailsDto
    {
        public int PlanId { get; set; }
        public int UserId { get; set; }
        public string? MenuDescription { get; set; }
        public string? FullDescription { get; set; }
        public string Name { get; set; }
        public List<ExerciseFrequencyDto> ExerciseFrequencies { get; set; }
        public List<Exercise> Exercises { get; set; }
        public List<Frequency> Frequencies { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? TagsString { get; set; }
        public double? AverageRating { get; set; }
        public bool? IsSubscribed { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Application.Models.Plan
{
    public record class GetPlansOnIndexDto
    {
        public int PlanId { get; set; }
        public int UserId { get; set; }
        public string? MenuDescription { get; set; }
        public string? FullDescription { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Tags { get; set; }
        public double? AverageRating { get; set; }
    }
}

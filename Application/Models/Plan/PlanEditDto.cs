using GymPlanner.Domain.Entities.Plans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Application.Models.Plan
{
    public record class PlanEditDto
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
    }

    public record class ExerciseFrequencyDto
    {
        public int Id { get; set; }
        public int ExerciseId { get; set; }
        public int FrequencyId { get; set; }
        public string Description { get; set; }
    }
    public record class ExerciseDto
    {
        public int PlanId { get; set; }
        public string Name { get; set; }
    }    
    //Могут добавиться изменения, поэтому да, создаю два одинаковых класса, чтобы потом их не создавать. мало ли. Может быть это неправильно.
    public record class FrequencyDto
    {
        public int PlanId { get; set; }
        public string Name { get; set; }
    }
}

using GymPlanner.Domain.Entities.Identity;

namespace GymPlanner.Domain.Entities.Plans
{
    public class Plan : BaseEntity
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public IList<PlanExcersiseFrequency>? planExcersiseFrequencies { get; set; }
    }
}

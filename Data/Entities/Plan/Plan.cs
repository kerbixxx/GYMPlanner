namespace GymPlanner.Domain.Entities.Plan
{
    public class Plan : BaseEntity
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<PlanExcersiseFrequency>? planExcersiseFrequencies { get; set; }
    }
}

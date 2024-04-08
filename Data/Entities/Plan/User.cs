namespace GymPlanner.Domain.Entities.Plan
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Plan>? Plans { get; set; }
    }
}

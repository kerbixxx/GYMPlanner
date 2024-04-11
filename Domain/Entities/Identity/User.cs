using Microsoft.AspNetCore.Identity;
using GymPlanner.Domain.Entities.Plans;

namespace GymPlanner.Domain.Entities.Identity
{
    public class User : BaseEntity
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Plan>? Plans { get; set; }
    }
}

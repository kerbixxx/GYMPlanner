using Microsoft.AspNetCore.Identity;
using GymPlanner.Domain.Entities.Plans;

namespace GymPlanner.Domain.Entities.Identity
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; }
        public ICollection<Plan>? Plans { get; set; }
    }
}

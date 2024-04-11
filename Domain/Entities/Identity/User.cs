using Microsoft.AspNetCore.Identity;
using GymPlanner.Domain.Entities.Plans;

namespace GymPlanner.Domain.Entities.Identity
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role? Role { get; set; }
        public ICollection<Plan>? Plans { get; set; }
    }
}

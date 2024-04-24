using GymPlanner.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Domain.Entities.Plans
{
    public class Subscription
    {
        public int UserId { get; set; }
        public User? User { get; set; }
        public int PlanId { get; set; }
        public Plan? Plan { get; set; }
    }
}

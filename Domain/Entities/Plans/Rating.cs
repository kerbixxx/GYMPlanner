using GymPlanner.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Domain.Entities.Plans
{
    public class Rating
    {
        public int PlanId { get; set; }
        public virtual Plan? Plan { get; set; }
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public int Vote { get; set; }
    }
}

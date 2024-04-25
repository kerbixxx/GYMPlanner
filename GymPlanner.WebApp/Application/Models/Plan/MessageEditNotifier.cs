using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Application.Models.Plan
{
    public record class MessageEditNotifier
    {
        public string SubscriberEmail { get; set; }
        public string PlanName { get; set; }
    }
}

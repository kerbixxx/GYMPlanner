using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Application.Interfaces.Services
{
    public interface ISubscriptionService
    {
        Task NotifyAllSubscribers(int planId);
        bool IsUserSubbedToPlan(int userId, int planId);
    }
}

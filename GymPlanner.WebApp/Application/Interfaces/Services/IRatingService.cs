using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Application.Interfaces.Services
{
    public interface IRatingService
    {
        Task RatePlanAsync(int planId, int userId, int rating);
        Task<double> GetAverageRatingForPlan(int planId);
    }
}

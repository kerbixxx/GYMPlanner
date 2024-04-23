using GymPlanner.Application.Interfaces.Repositories.Plan;
using GymPlanner.Application.Interfaces.Services;
using GymPlanner.Domain.Entities.Plans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Application.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepo;
        public RatingService(IRatingRepository ratingRepo)
        {
            _ratingRepo = ratingRepo;
        }

        public async Task<double> GetAverageRatingForPlan(int planId)
        {
            var ratings = await _ratingRepo.GetRatingsForPlan(planId);
            if (ratings.Any())
            {
                double averageRating = ratings.Average(r => r.Vote);
                return averageRating;
            }
            else
            {
                return 0;
            }
        }

        public async Task RatePlanAsync(int planId, int userId, int rating)
        {
            var obj = _ratingRepo.FirstOrDefault(r => (r.PlanId == planId) && (r.UserId == userId));
            if (obj == null)
            {
                obj = new Rating()
                {
                    PlanId = planId,
                    UserId = userId,
                    Vote = rating
                };
                await _ratingRepo.AddAsync(obj);
            }
            else
            {
                obj.Vote = rating;
                await _ratingRepo.UpdateAsync(obj);
            }
        }
    }
}

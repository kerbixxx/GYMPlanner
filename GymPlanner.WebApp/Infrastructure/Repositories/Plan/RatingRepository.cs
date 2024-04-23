using GymPlanner.Application.Interfaces.Repositories.Plan;
using GymPlanner.Domain.Entities.Plans;
using GymPlanner.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Infrastructure.Repositories.Plan
{
    public class RatingRepository : Repository<Rating>, IRatingRepository
    {
        private readonly PlanDbContext _db;
        public RatingRepository(PlanDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Rating>> GetRatingsForPlan(int planId)
        {
            return await _db.Ratings.Where(r => r.PlanId == planId).ToListAsync();
        }
    }
}

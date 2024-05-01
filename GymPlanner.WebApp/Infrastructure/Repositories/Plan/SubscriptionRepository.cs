using GymPlanner.Application.Interfaces.Repositories.Plan;
using GymPlanner.Domain.Entities.Identity;
using GymPlanner.Domain.Entities.Plans;
using GymPlanner.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Infrastructure.Repositories.Plan
{
    public class SubscriptionRepository : Repository<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(PlanDbContext db) : base(db)
        {
        }

        public async Task<List<Subscription>> GetSubscriptionsOnPlanAsync(int id)
        {
            var objlist = await _db.Subscriptions
                    .Where(s => s.PlanId == id)
                    .Include(s => s.User)
                    .Select(s => new Subscription
                    {
                        UserId = s.UserId,
                        User = new User { Email = s.User.Email },
                        PlanId = s.PlanId,
                        Plan = new Domain.Entities.Plans.Plan { Name = s.Plan.Name }
                    })
                    .ToListAsync();
            return objlist;
        }
    }
}

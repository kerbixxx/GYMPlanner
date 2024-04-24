using GymPlanner.Application.Interfaces.Repositories.Plan;
using GymPlanner.Domain.Entities.Plans;
using GymPlanner.Infrastructure.Contexts;
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
        private readonly PlanDbContext _db;
        public SubscriptionRepository(PlanDbContext db) : base(db)
        {
            _db = db;
        }
    }
}

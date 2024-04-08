using GymPlanner.Application.Interfaces.Repositories;
using GymPlanner.Infrastructure.Contexts;
using GymPlanner.Domain.Entities.Plan;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Infrastructure.Repositories
{
    public class PlanRepository : Repository<Plan>, IPlanRepository
    {
        private readonly ApplicationDbContext _db;
        public PlanRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public override Plan Get(int id)
        {
            return _db.Plans.Include(p => p.planExcersiseFrequencies).FirstOrDefault(p => p.Id == id);
        }
    }
}

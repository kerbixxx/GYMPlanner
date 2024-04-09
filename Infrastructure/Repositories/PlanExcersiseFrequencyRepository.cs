using GymPlanner.Application.Interfaces.Repositories;
using GymPlanner.Domain.Entities.Plan;
using GymPlanner.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Infrastructure.Repositories
{
    public class PlanExcersiseFrequencyRepository : Repository<PlanExcersiseFrequency>, IPlanExcersiseFrequencyRepository
    {
        private readonly ApplicationDbContext _db;
        public PlanExcersiseFrequencyRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }
    }
}

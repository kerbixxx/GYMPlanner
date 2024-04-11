using GymPlanner.Application.Interfaces.Repositories;
using GymPlanner.Infrastructure.Contexts;
using GymPlanner.Domain.Entities.Plans;
using Microsoft.EntityFrameworkCore;

namespace GymPlanner.Infrastructure.Repositories
{
    public class PlanRepository : Repository<Plan>, IPlanRepository
    {
        private readonly PlanDbContext _db;
        public PlanRepository(PlanDbContext db) : base(db)
        {
            _db = db;
        }

        public override async Task<Plan> Get(int id)
        {
            return await _db.Plans.Include(p => p.planExcersiseFrequencies)
                    .ThenInclude(p=>p.Frequency)
                .Include(p=>p.planExcersiseFrequencies)
                    .ThenInclude(p=>p.Excersise).
                FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Plan>> GetAll()
        {
            return await _db.Plans.ToListAsync();
        }
    }
}

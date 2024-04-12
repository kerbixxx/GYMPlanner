using GymPlanner.Domain.Entities.Plans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Application.Interfaces.Repositories.Plan
{
    public interface IPlanRepository : IRepository<GymPlanner.Domain.Entities.Plans.Plan>
    {
        Task<List<GymPlanner.Domain.Entities.Plans.Plan>> GetAll();
    }
}

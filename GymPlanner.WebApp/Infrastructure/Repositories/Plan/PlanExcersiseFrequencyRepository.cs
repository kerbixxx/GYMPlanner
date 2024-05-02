﻿using GymPlanner.Application.Interfaces.Repositories.Plan;
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
    public class PlanExcersiseFrequencyRepository : Repository<PlanExerciseFrequency>, IPlanExerciseFrequencyRepository
    {
        public PlanExcersiseFrequencyRepository(PlanDbContext db) : base(db)
        {
        }

        public async Task<List<PlanExerciseFrequency>> GetByPlanId(int planId)
        {
            return await _db.PlanExerciseFrequencies.Where(f => f.PlanId == planId).ToListAsync();
        }
    }
}

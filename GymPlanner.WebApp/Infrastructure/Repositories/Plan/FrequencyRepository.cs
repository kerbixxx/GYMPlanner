﻿using GymPlanner.Application.Interfaces.Repositories.Plan;
using GymPlanner.Domain.Entities.Plans;
using GymPlanner.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Infrastructure.Repositories.Plan
{
    public class FrequencyRepository : Repository<Frequency>, IFrequencyRepository
    {
        public FrequencyRepository(PlanDbContext db) : base(db)
        {
        }
    }
}

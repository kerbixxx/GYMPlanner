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
    public class ExerciseRepository : Repository<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(PlanDbContext db) : base(db)
        {
        }
    }
}

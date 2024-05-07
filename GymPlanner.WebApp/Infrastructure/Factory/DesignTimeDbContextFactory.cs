using GymPlanner.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Infrastructure.Factory
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PlanDbContext>
    {
        public PlanDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PlanDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost,1439;Database=gym;User Id=sa;Password=123456@A;Encrypt=True;");

            return new PlanDbContext(optionsBuilder.Options);
        }
    }
}

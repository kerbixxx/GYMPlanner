using Data.Entities.Plan;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Excersise> Excersises { get; set; }
        public DbSet<Frequency> Frequencies { get; set; }
        public DbSet<PlanExcersiseFrequency> PlanExcersiseFrequencys { get;set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}

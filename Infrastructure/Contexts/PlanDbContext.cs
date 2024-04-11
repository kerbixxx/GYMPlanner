using GymPlanner.Domain.Entities.Identity;
using GymPlanner.Domain.Entities.Plans;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using System.Diagnostics.Metrics;

namespace GymPlanner.Infrastructure.Contexts
{
    public class PlanDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Excersise> Excersises { get; set; }
        public DbSet<Frequency> Frequencies { get; set; }
        public DbSet<PlanExcersiseFrequency> PlanExcersiseFrequencys { get;set; }

        public PlanDbContext(DbContextOptions<PlanDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(k => k.Id);

            modelBuilder.Entity<User>()
                .Property(u => u.DisplayName)
                .HasMaxLength(70)
                .IsRequired();

            var adminUser = new User
            {
                Id = 1,
                DisplayName = "admin",
                Email = "admin@example.com",
                Password = "admin1234"
            };

            modelBuilder.Entity<User>().HasData(adminUser);

            modelBuilder.Entity<PlanExcersiseFrequency>()
                .HasKey(k => new { k.PlanId, k.FrequencyId, k.ExcersiseId, k.Id });

            modelBuilder.Entity<Plan>()
                .HasKey(k => k.Id);

            modelBuilder.Entity<Plan>()
                .Property(p => p.Name)
                .HasMaxLength(400)
                .IsRequired();

            modelBuilder.Entity<Excersise>()
                .HasKey(k => k.Id);

            modelBuilder.Entity<Excersise>()
                .Property(e => e.Name)
                .HasMaxLength(400)
                .IsRequired();

            modelBuilder.Entity<Frequency>()
                .HasKey(k => k.Id);

            modelBuilder.Entity<Frequency>()
                .Property(f => f.Name)
                .HasMaxLength(30)
                .IsRequired();

            modelBuilder.Entity<PlanExcersiseFrequency>()
                .HasOne(p => p.Plan)
                .WithMany(p => p.planExcersiseFrequencies);

            modelBuilder.Entity<PlanExcersiseFrequency>()
                .Property(pef => pef.Description)
                .HasMaxLength(60);

            modelBuilder.Entity<PlanExcersiseFrequency>()
                .Property(pef => pef.Id)
                .ValueGeneratedOnAdd();

            Plan plan = new() { Id = 1, Name = "План 1", UserId = 1 };
            modelBuilder.Entity<Plan>().HasData(plan);

            Excersise excersise = new() { Id = 1, Name = "Упражнение 1" };
            modelBuilder.Entity<Excersise>().HasData(excersise);

            Frequency frequency = new() { Id = 1, Name = "Частота 1" };
            modelBuilder.Entity<Frequency>().HasData(frequency);

            PlanExcersiseFrequency pef = new() { Id = 1, PlanId = 1, ExcersiseId = 1, FrequencyId = 1, Description = "15" };
            modelBuilder.Entity<PlanExcersiseFrequency>().HasData(pef);
        }
    }
}

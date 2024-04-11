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
        public DbSet<Role> Roles { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Excersise> Excersises { get; set; }
        public DbSet<Frequency> Frequencies { get; set; }
        public DbSet<PlanExcersiseFrequency> PlanExcersiseFrequencys { get;set; }

        public PlanDbContext(DbContextOptions<PlanDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(k => k.Id);

            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminEmail = "admin@mail.ru";
            string adminPassword = "123456";

            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            User adminUser = new User { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });

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

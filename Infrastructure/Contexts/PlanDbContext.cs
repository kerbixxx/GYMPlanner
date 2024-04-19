using GymPlanner.Domain.Entities.Chat;
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
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Frequency> Frequencies { get; set; }
        public DbSet<PlanExerciseFrequency> PlanExerciseFrequencies { get;set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<Tag> Tags { get; set; }

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

            modelBuilder.Entity<Tag>().HasKey(k => k.Id);

            modelBuilder.Entity<Dialog>().HasKey(d => d.Id);
            modelBuilder.Entity<Dialog>()
                .HasOne(d => d.User)
                .WithMany() 
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Dialog>()
                .HasOne(d => d.OtherUser)
                .WithMany() 
                .HasForeignKey(d => d.OtherUserId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Dialog)
                .WithMany(d => d.Messages);

            modelBuilder.Entity<Message>().HasKey(k => k.Id);

            modelBuilder.Entity<Message>().Property(p => p.Content)
                .HasMaxLength(2077)
                .IsRequired();

            modelBuilder.Entity<PlanExerciseFrequency>()
                .HasKey(k => new { k.PlanId, k.FrequencyId, k.ExerciseId, k.Id });

            modelBuilder.Entity<Plan>()
                .HasKey(k => k.Id);

            modelBuilder.Entity<Plan>()
                .Property(p => p.Name)
                .HasMaxLength(400)
                .IsRequired();

            modelBuilder.Entity<Plan>().Property(d => d.MenuDescription).HasMaxLength(300);

            modelBuilder.Entity<Exercise>()
                .HasKey(k => k.Id);

            modelBuilder.Entity<Exercise>()
                .Property(e => e.Name)
                .HasMaxLength(400)
                .IsRequired();

            modelBuilder.Entity<Frequency>()
                .HasKey(k => k.Id);

            modelBuilder.Entity<Frequency>()
                .Property(f => f.Name)
                .HasMaxLength(30)
                .IsRequired();

            modelBuilder.Entity<PlanExerciseFrequency>()
                .HasOne(p => p.Plan)
                .WithMany(p => p.planExersiseFrequencies);

            modelBuilder.Entity<PlanExerciseFrequency>()
                .Property(pef => pef.Description)
                .HasMaxLength(60);

            modelBuilder.Entity<PlanExerciseFrequency>()
                .Property(pef => pef.Id)
                .ValueGeneratedOnAdd();

            Plan plan = new() { Id = 1, Name = "План 1", UserId = 1 };
            modelBuilder.Entity<Plan>().HasData(plan);

            Exercise excersise = new() { Id = 1, Name = "Упражнение 1" };
            modelBuilder.Entity<Exercise>().HasData(excersise);

            Frequency frequency = new() { Id = 1, Name = "Частота 1" };
            modelBuilder.Entity<Frequency>().HasData(frequency);

            PlanExerciseFrequency pef = new() { Id = 1, PlanId = 1, ExerciseId = 1, FrequencyId = 1, Description = "15" };
            modelBuilder.Entity<PlanExerciseFrequency>().HasData(pef);
        }
    }
}

﻿using GymPlanner.Domain.Entities.Plan;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace GymPlanner.Domain.Contexts
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer();

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Excersise> Excersises { get; set; }
        public DbSet<Frequency> Frequencies { get; set; }
        public DbSet<PlanExcersiseFrequency> PlanExcersiseFrequencys { get;set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PlanExcersiseFrequency>()
                .HasKey(k => new { k.PlanId, k.FrequencyId, k.ExcersiseId });

            modelBuilder.Entity<User>()
                .HasKey(k => k.Id);

            modelBuilder.Entity<User>()
                .Property(u => u.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.LastName)
                .HasMaxLength(50)
                .IsRequired();

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
        }
    }
}

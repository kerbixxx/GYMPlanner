using GymPlanner.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Infrastructure.Contexts
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
            .HasKey(k => k.Id);

            modelBuilder.Entity<User>()
                .Property(u => u.DisplayName)
                .HasMaxLength(70)
                .IsRequired();

            var adminUser = new User
            {
                DisplayName = "admin",
                Email = "admin@example.com",
                Password = "admin1234"
            };

            modelBuilder.Entity<User>().HasData(adminUser);
        }
    }
}

﻿// <auto-generated />
using System;
using GymPlanner.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GymPlanner.Infrastructure.Migrations
{
    [DbContext(typeof(PlanDbContext))]
    [Migration("20240419081646_PlanCreatedAt")]
    partial class PlanCreatedAt
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GymPlanner.Domain.Entities.Chat.Dialog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OtherUserId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OtherUserId");

                    b.HasIndex("UserId");

                    b.ToTable("Dialogs");
                });

            modelBuilder.Entity("GymPlanner.Domain.Entities.Chat.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(2077)
                        .HasColumnType("nvarchar(2077)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("DialogId")
                        .HasColumnType("int");

                    b.Property<int>("UserIdFrom")
                        .HasColumnType("int");

                    b.Property<int>("UserIdTo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DialogId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("GymPlanner.Domain.Entities.Identity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "user"
                        });
                });

            modelBuilder.Entity("GymPlanner.Domain.Entities.Identity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@mail.ru",
                            Password = "123456",
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("GymPlanner.Domain.Entities.Plans.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.HasKey("Id");

                    b.ToTable("Exercises");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Упражнение 1"
                        });
                });

            modelBuilder.Entity("GymPlanner.Domain.Entities.Plans.Frequency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Frequencies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Частота 1"
                        });
                });

            modelBuilder.Entity("GymPlanner.Domain.Entities.Plans.Plan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MenuDescription")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Plans");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "План 1",
                            UserId = 1
                        });
                });

            modelBuilder.Entity("GymPlanner.Domain.Entities.Plans.PlanExerciseFrequency", b =>
                {
                    b.Property<int>("PlanId")
                        .HasColumnType("int");

                    b.Property<int>("FrequencyId")
                        .HasColumnType("int");

                    b.Property<int>("ExerciseId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("PlanId", "FrequencyId", "ExerciseId", "Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("FrequencyId");

                    b.ToTable("PlanExerciseFrequencies");

                    b.HasData(
                        new
                        {
                            PlanId = 1,
                            FrequencyId = 1,
                            ExerciseId = 1,
                            Id = 1,
                            Description = "15"
                        });
                });

            modelBuilder.Entity("GymPlanner.Domain.Entities.Plans.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PlanId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlanId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("GymPlanner.Domain.Entities.Chat.Dialog", b =>
                {
                    b.HasOne("GymPlanner.Domain.Entities.Identity.User", "OtherUser")
                        .WithMany()
                        .HasForeignKey("OtherUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GymPlanner.Domain.Entities.Identity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("OtherUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GymPlanner.Domain.Entities.Chat.Message", b =>
                {
                    b.HasOne("GymPlanner.Domain.Entities.Chat.Dialog", "Dialog")
                        .WithMany("Messages")
                        .HasForeignKey("DialogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dialog");
                });

            modelBuilder.Entity("GymPlanner.Domain.Entities.Identity.User", b =>
                {
                    b.HasOne("GymPlanner.Domain.Entities.Identity.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("GymPlanner.Domain.Entities.Plans.Plan", b =>
                {
                    b.HasOne("GymPlanner.Domain.Entities.Identity.User", "User")
                        .WithMany("Plans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GymPlanner.Domain.Entities.Plans.PlanExerciseFrequency", b =>
                {
                    b.HasOne("GymPlanner.Domain.Entities.Plans.Exercise", "Exercise")
                        .WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GymPlanner.Domain.Entities.Plans.Frequency", "Frequency")
                        .WithMany()
                        .HasForeignKey("FrequencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GymPlanner.Domain.Entities.Plans.Plan", "Plan")
                        .WithMany("planExersiseFrequencies")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");

                    b.Navigation("Frequency");

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("GymPlanner.Domain.Entities.Plans.Tag", b =>
                {
                    b.HasOne("GymPlanner.Domain.Entities.Plans.Plan", null)
                        .WithMany("Tags")
                        .HasForeignKey("PlanId");
                });

            modelBuilder.Entity("GymPlanner.Domain.Entities.Chat.Dialog", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("GymPlanner.Domain.Entities.Identity.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("GymPlanner.Domain.Entities.Identity.User", b =>
                {
                    b.Navigation("Plans");
                });

            modelBuilder.Entity("GymPlanner.Domain.Entities.Plans.Plan", b =>
                {
                    b.Navigation("Tags");

                    b.Navigation("planExersiseFrequencies");
                });
#pragma warning restore 612, 618
        }
    }
}

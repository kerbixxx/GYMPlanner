﻿using GymPlanner.Domain.Entities.Identity;

namespace GymPlanner.Domain.Entities.Plans
{
    public class Plan : BaseEntity
    {
        public string Name { get; set; }
        public string? MenuDescription { get; set; }
        public string? FullDescription { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public IList<PlanExerciseFrequency>? planExersiseFrequencies { get; set; }
        public List<Tag>? Tags { get; set; }
    }
}

﻿namespace Data.Entities.Plan
{
    public class Excersise : BaseEntity
    {
        public string Name { get; set; }
        public int PlanExcersiseFrequencyId { get; set; }
        public PlanExcersiseFrequency PlanExcersiseFrequency { get; set; }
    }
}

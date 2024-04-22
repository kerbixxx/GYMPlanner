using GymPlanner.Domain.Entities.Identity;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string? TagsDb { get; set; }
        [NotMapped]
        public string[] Tags
        {
            get
            {
                if (TagsDb != null)
                {
                    var tab = this.TagsDb.Split(',');
                    return tab.ToArray();
                }
                return new string[0];
            }
            set
            {
                this.TagsDb = string.Join(",", value);
            }
        }
        public DateTime? CreatedAt { get; set; }
    }
}

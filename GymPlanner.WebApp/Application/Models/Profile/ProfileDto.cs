using GymPlanner.Domain.Entities.Identity;

namespace GymPlanner.Application.Models.Profile
{
    public record class ProfileDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public List<Domain.Entities.Plans.Plan>? Plans { get; set; }
    }
}

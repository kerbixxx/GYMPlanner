using System.ComponentModel.DataAnnotations;

namespace GymPlanner.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
    }
}

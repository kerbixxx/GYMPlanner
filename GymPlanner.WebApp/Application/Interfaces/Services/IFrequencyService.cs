using GymPlanner.Application.Models.Plan;
using GymPlanner.Domain.Entities.Plans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Application.Interfaces.Services
{
    public interface IFrequencyService
    {
        Task<int> AddFrequencyToPlan(FrequencyDto dto);
        Task DeleteFrequencyFromPlan(int id);
        Task UpdateAsync(Frequency obj);
    }
}

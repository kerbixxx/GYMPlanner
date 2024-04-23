using GymPlanner.Application.Interfaces.Repositories;
using GymPlanner.Application.Models.Profile;
using GymPlanner.Domain.Entities.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymPlanner.WebUI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserRepository _userRepository;
        public ProfileController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IActionResult> Index(int? id, string? name)
        {
            User user = new User();
            if (id != null) user = await _userRepository.GetProfileAsync(id);
            else if (name != null) user = await _userRepository.FindByNameAsync(name);
            else { return BadRequest(); }
            var profileDto = new ProfileDto()
            {
                Plans = user.Plans.ToList(),
                UserId = user.Id,
                Name = user.Email
            };
            return View(profileDto);
        }        
    }
}

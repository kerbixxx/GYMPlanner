using GymPlanner.Application.Interfaces.Repositories;
using GymPlanner.Domain.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymPlanner.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepo;
        public UserController(IUserRepository userRepo) 
        {
            _userRepo = userRepo;
        }

        public async Task<IActionResult> Index()
        {
            var userList = await _userRepo.GetAllUsersAsync();
            return View(userList);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userRepo.GetAsync(id);
            if (user != null) return View(user);
            return BadRequest();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User user)
        {
            if (ModelState.IsValid)
            {
                await _userRepo.UpdateAsync(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }
    }
}

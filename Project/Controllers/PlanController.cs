using GymPlanner.Application.Interfaces.Repositories;
using GymPlanner.Domain.Entities.Plan;
using Microsoft.AspNetCore.Mvc;

namespace GymPlanner.WebUI.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanRepository _planRepo;
        private readonly IPlanExcersiseFrequencyRepository _pefRepo;
        public PlanController(IPlanRepository planRepo, IPlanExcersiseFrequencyRepository pefRepo)
        {
            _planRepo = planRepo;
            _pefRepo = pefRepo;
        }
        public async Task<IActionResult> Index()
        {
            var plans = await _planRepo.GetAll();
            return View(plans);
        }

        public async Task<IActionResult> Details(int id)
        {
            var plan = await _planRepo.Get(id);
            return View(plan);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var plan = await _planRepo.Get(id);
            return View(plan);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Plan plan)
        {
            plan.UserId = 1;
            if (ModelState.IsValid)
            {
                _planRepo.Update(plan);
                await _planRepo.Save();
                return RedirectToAction("Index");
            }
            return await Edit(plan);
        }
        public IActionResult Create()
        {
            var plan = new Plan();
            return View(plan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Plan plan)
        {
            plan.UserId = 1; // На данный момент костыль. Потом нужно будет смотреть через UserManager кто создает план.
            if(ModelState.IsValid)
            {
                await _planRepo.Add(plan);
                await _planRepo.Save();
                return RedirectToAction("Index");
            }
            return View(plan);
        }
    }
}

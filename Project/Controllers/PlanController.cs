using GymPlanner.Application.Interfaces.Repositories;
using GymPlanner.Application.Models.Plan;
using GymPlanner.Domain.Entities.Plans;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var plan = await _planRepo.Get(id);
            var planDto = new PlanEditDto()
            {
                Excersises = plan.planExcersiseFrequencies.Select(p => p.Excersise).ToList(),
                Frequencies = plan.planExcersiseFrequencies.Select(p => p.Frequency).ToList(),
                PlanId = plan.Id
            };
            return View(planDto);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(PlanEditDto planDto)
        {
            if (ModelState.IsValid)
            {
                //_planRepo.Update(plan);
                await _planRepo.Save();
                return RedirectToAction("Index");
            }
            return await Edit(planDto);
        }
        [Authorize]
        public IActionResult Create()
        {
            var plan = new Plan();
            return View(plan);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Plan plan)
        {
            plan.UserId = 1; // На данный момент костыль. Потом нужно будет смотреть через User.Identity кто создает план.
            if(ModelState.IsValid)
            {
                await _planRepo.Add(plan);
                await _planRepo.Save();
                return RedirectToAction("Index");
            }
            return View(plan);
        }
        [Authorize]
        [HttpPost]
        public IActionResult CalculateAdjacentCells(Plan model)
        {
            var frequencies = model.planExcersiseFrequencies.Select(p => p.Frequency).ToList();
            var excersises = model.planExcersiseFrequencies.Select(p => p.Excersise).ToList();
            List<PlanExcersiseFrequency> pefList = new();
            for (int i = 0; i < frequencies.Count(); i++)
            {
                for (int j = 0; j < excersises.Count(); j++)
                {
                    var pef = model.planExcersiseFrequencies.FirstOrDefault(pef => pef.Frequency == frequencies[i] && pef.Excersise == excersises[j]);
                    if (pef == null)
                        pefList.Add(new()
                        {
                            Plan = model,
                            PlanId = model.Id,
                            Frequency = frequencies[i],
                            FrequencyId = frequencies[i].Id,
                            Excersise = excersises[j],
                            ExcersiseId = excersises[j].Id,
                            Description = "0"
                        });
                    else pefList.Add(pef);
                }
            }
            return Json(pefList);
        }
    }
}

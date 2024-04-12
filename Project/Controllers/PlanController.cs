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
            var plan = await _planRepo.GetAsync(id);
            if (plan == null) return BadRequest();
            return View(plan);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var plan = await _planRepo.GetAsync(id);
            if (plan == null) return BadRequest();
            var excfreqList = new List<ExerciseFrequencyDto>();
            foreach (var pef in plan.planExcersiseFrequencies)
            {
                var excfreq = new ExerciseFrequencyDto()
                {
                    Description = pef.Description,
                    ExerciseName = pef.Excersise.Name,
                    ExerciseId = pef.Excersise.Id,
                    FrequencyId = pef.FrequencyId,
                    FrequencyName = pef.Frequency.Name
                };
                excfreqList.Add(excfreq);
            }
            var planDto = new PlanEditDto()
            {
                PlanId = plan.Id,
                ExerciseFrequencies = excfreqList,
                Name = plan.Name
            };
            return View(planDto);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(PlanEditDto planDto)
        {
            //if (ModelState.IsValid)
            //{
            //    var plan = new Plan()
            //    {
            //        planExcersiseFrequencies = planDto.PlanExcersiseFrequencies,
            //        Id = planDto.PlanId,
            //        Name = planDto.Name
            //    };
            //    await _planRepo.UpdateAsync(plan);
            //    return RedirectToAction("Index");
            //}
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
            if (ModelState.IsValid)
            {
                await _planRepo.AddAsync(plan);
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

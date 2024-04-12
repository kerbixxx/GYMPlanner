using GymPlanner.Application.Interfaces.Repositories.Plan;
using GymPlanner.Application.Models.Plan;
using GymPlanner.Domain.Entities.Plans;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymPlanner.WebUI.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanRepository _planRepo;
        private readonly IPlanExerciseFrequencyRepository _pefRepo;
        public PlanController(IPlanRepository planRepo, IPlanExerciseFrequencyRepository pefRepo)
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
            foreach (var pef in plan.planExersiseFrequencies)
            {
                var excfreq = new ExerciseFrequencyDto()
                {
                    Description = pef.Description,
                    ExerciseId = pef.Exercise.Id,
                    FrequencyId = pef.FrequencyId,
                };
                excfreqList.Add(excfreq);
            }
            var planDto = new PlanEditDto()
            {
                PlanId = plan.Id,
                ExerciseFrequencies = excfreqList,
                Name = plan.Name,
                Excersises = plan.planExersiseFrequencies.Select(pef => pef.Exercise).Distinct().ToList(),
                Frequencies = plan.planExersiseFrequencies.Select(pef => pef.Frequency).Distinct().ToList()
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

        public IActionResult AddExersiseModal(int planId)
        {
            var dto = new ExerciseDto() { PlanId = planId, Name = "" };
            return PartialView("AddExersiseModal",dto);
        }
        [HttpPost]
        public async Task<IActionResult> AddExersise(ExerciseDto dto)
        {

            return View();
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
            var frequencies = model.planExersiseFrequencies.Select(p => p.Frequency).ToList();
            var excersises = model.planExersiseFrequencies.Select(p => p.Exercise).ToList();
            List<PlanExerciseFrequency> pefList = new();
            for (int i = 0; i < frequencies.Count(); i++)
            {
                for (int j = 0; j < excersises.Count(); j++)
                {
                    var pef = model.planExersiseFrequencies.FirstOrDefault(pef => pef.Frequency == frequencies[i] && pef.Exercise == excersises[j]);
                    if (pef == null)
                        pefList.Add(new()
                        {
                            Plan = model,
                            PlanId = model.Id,
                            Frequency = frequencies[i],
                            FrequencyId = frequencies[i].Id,
                            Exercise = excersises[j],
                            ExerciseId = excersises[j].Id,
                            Description = "0"
                        });
                    else pefList.Add(pef);
                }
            }
            return Json(pefList);
        }
    }
}

using GymPlanner.Application.Interfaces.Repositories;
using GymPlanner.Application.Interfaces.Repositories.Plan;
using GymPlanner.Application.Models.Plan;
using GymPlanner.Domain.Entities.Plans;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography.Xml;

namespace GymPlanner.WebUI.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanRepository _planRepo;
        private readonly IPlanExerciseFrequencyRepository _pefRepo;
        private readonly IExerciseRepository _exerciseRepo;
        private readonly IFrequencyRepository _frequencyRepo;
        private readonly IUserRepository _userRepo;
        public PlanController(IPlanRepository planRepo, IPlanExerciseFrequencyRepository pefRepo, IExerciseRepository exerciseRepo, IFrequencyRepository frequencyRepo, IUserRepository userRepo)
        {
            _planRepo = planRepo;
            _pefRepo = pefRepo;
            _exerciseRepo = exerciseRepo;
            _frequencyRepo = frequencyRepo;
            _userRepo = userRepo;
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
                    Id = pef.Id
                };
                excfreqList.Add(excfreq);
            }
            var planDto = new PlanEditDto()
            {
                PlanId = plan.Id,
                ExerciseFrequencies = excfreqList,
                Name = plan.Name,
                Exercises = plan.planExersiseFrequencies.Select(pef => pef.Exercise).Distinct().ToList(),
                Frequencies = plan.planExersiseFrequencies.Select(pef => pef.Frequency).Distinct().ToList(),
                UserId = plan.UserId
            };

            if (plan.planExersiseFrequencies.Count == 0)
            {
                Exercise exercise = new()
                {
                    Name = "Упражнение 1"
                };
                Frequency frequency = new()
                {
                    Name = "Частота 1"
                };
                await _exerciseRepo.AddAsync(exercise);
                await _frequencyRepo.AddAsync(frequency);
                PlanExerciseFrequency pef = new()
                {
                    PlanId = plan.Id,
                    FrequencyId = frequency.Id,
                    ExerciseId = exercise.Id,
                    Description = "0"
                };
                await _pefRepo.AddAsync(pef);
            }

            return View(planDto);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(PlanEditDto planDto)
        {
            if (ModelState.IsValid)
            {
                var plan = new Plan()
                {
                    Id = planDto.PlanId,
                    Name = planDto.Name,
                    UserId = planDto.UserId,
                };
                await _planRepo.UpdateAsync(plan);
                foreach(var pef in planDto.ExerciseFrequencies)
                {
                    await _pefRepo.UpdateAsync(new PlanExerciseFrequency() { Id = pef.Id,ExerciseId = pef.ExerciseId,PlanId=planDto.PlanId,FrequencyId=pef.FrequencyId,Description=pef.Description});
                }
                foreach(var excersise in planDto.Exercises)
                {
                    await _exerciseRepo.UpdateAsync(excersise);
                }
                foreach (var frequency in planDto.Frequencies)
                {
                    await _frequencyRepo.UpdateAsync(frequency);
                }
                return RedirectToAction("Index");
            }
            return await Edit(planDto);
        }

        public IActionResult AddExerciseModal(int planId)
        {
            var dto = new ExerciseDto() { PlanId = planId, Name = "" };
            return PartialView("AddExerciseModal",dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddExerсise(ExerciseDto dto)
        {
            var plan = await _planRepo.GetAsync(dto.PlanId);
            Exercise exercise = new() { Name = dto.Name };
            await _exerciseRepo.AddAsync(exercise);
            var pefList = new List<PlanExerciseFrequency>();
            foreach(var freq in plan.planExersiseFrequencies.Select(p => p.FrequencyId).Distinct())
            {
                pefList.Add(new PlanExerciseFrequency()
                {
                    FrequencyId = freq,
                    ExerciseId = exercise.Id,
                    PlanId = plan.Id,
                    Description = "0"
                });
            }
            foreach(var pef in pefList)
            {
                await _pefRepo.AddAsync(pef);
            }
            return RedirectToAction("Edit", new { Id = dto.PlanId});
        }

        public IActionResult AddFrequencyModal(int planId)
        {
            var dto = new FrequencyDto() { PlanId = planId, Name = "" };
            return PartialView("AddFrequencyModal", dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFrequency(FrequencyDto dto)
        {
            var plan = await _planRepo.GetAsync(dto.PlanId);
            Frequency frequency = new() { Name = dto.Name };
            await _frequencyRepo.AddAsync(frequency);
            var pefList = new List<PlanExerciseFrequency>();
            foreach (var exercise in plan.planExersiseFrequencies.Select(p => p.ExerciseId).Distinct())
            {
                pefList.Add(new PlanExerciseFrequency()
                {
                    FrequencyId = frequency.Id,
                    ExerciseId = exercise,
                    PlanId = plan.Id,
                    Description = "0"
                });
            }
            foreach (var pef in pefList)
            {
                await _pefRepo.AddAsync(pef);
            }
            return RedirectToAction("Edit", new { Id = dto.PlanId });
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
            var user = await _userRepo.FindByNameAsync(User.Identity.Name);
            plan.UserId = user.Id;
            if (ModelState.IsValid)
            {
                await _planRepo.AddAsync(plan);
                Exercise exercise = new()
                {
                    Name = "Упражнение 1"
                };
                Frequency frequency = new()
                {
                    Name = "Частота 1"
                };
                await _exerciseRepo.AddAsync(exercise);
                await _frequencyRepo.AddAsync(frequency);
                PlanExerciseFrequency pef = new()
                {
                    PlanId = plan.Id,
                    FrequencyId = frequency.Id,
                    ExerciseId = exercise.Id,
                    Description = "0"
                };
                await _pefRepo.AddAsync(pef);
                return RedirectToAction("Index");
            }
            return View(plan);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFrequency(int frequencyId)
        {
            var frequency = await _frequencyRepo.GetAsync(frequencyId);
            var pef = _pefRepo.FirstOrDefault(pef=>pef.FrequencyId == frequencyId);
            var planId = pef.PlanId;
            await _frequencyRepo.RemoveAsync(frequency);
            return RedirectToAction("Edit", new { Id = planId });
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteExercise(int exerciseId)
        {
            var exercise = await _exerciseRepo.GetAsync(exerciseId);
            var pef = _pefRepo.FirstOrDefault(pef=>pef.ExerciseId == exerciseId);
            var planId = pef.PlanId;
            await _exerciseRepo.RemoveAsync(exercise);
            return RedirectToAction("Edit",new { Id = planId });
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

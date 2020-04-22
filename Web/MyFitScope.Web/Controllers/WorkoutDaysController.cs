﻿namespace MyFitScope.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Services.Data;
    using MyFitScope.Web.ViewModels.WorkoutDays;

    public class WorkoutDaysController : Controller
    {
        private readonly IWorkoutDaysService workoutDaysService;

        public WorkoutDaysController(IWorkoutDaysService workoutDaysService)
        {
            this.workoutDaysService = workoutDaysService;
        }

        [HttpPost]
        public async Task<IActionResult> AddWorkoutDay(CreateWorkoutDayInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("CurrentWorkout", "Workouts");
            }

            await this.workoutDaysService.CreateWorkoutDayAsync(input.WorkoutId, input.WeekDay);

            return this.RedirectToAction("CurrentWorkout", "Workouts");
        }

        public IActionResult Edit(string workoutDayId)
        {
            var model = this.workoutDaysService.GetWorkoutDayById(workoutDayId);

            return this.View(model);
        }

        public async Task<IActionResult> Delete(string workoutDayId)
        {
            await this.workoutDaysService.DeleteWorkoutDayAsync(workoutDayId);

            return this.RedirectToAction("CurrentWorkout", "Workouts");
        }
    }
}

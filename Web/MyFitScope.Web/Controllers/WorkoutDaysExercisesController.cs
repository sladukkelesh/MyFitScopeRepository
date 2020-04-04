namespace MyFitScope.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Services.Data;
    using MyFitScope.Web.ViewModels.WorkoutDaysExercises;

    public class WorkoutDaysExercisesController : Controller
    {
        private readonly IWorkoutDaysExercisesService workoutDayExerciseService;

        public WorkoutDaysExercisesController(IWorkoutDaysExercisesService workoutDayExerciseService)
        {
            this.workoutDayExerciseService = workoutDayExerciseService;
        }

        public async Task<IActionResult> AddExerciseToWorkoutDay(AddExerciseToWorkoutDayInputModel input)
        {
            await this.workoutDayExerciseService.AddExerciseToWorkoutDayAsync(input.ExerciseId, input.WorkoutDayId);

            return this.RedirectToAction("Edit", "WorkoutDays", new { workoutDayId = input.WorkoutDayId });
        }

        public async Task<IActionResult> RemoveExercise(string exerciseId)
        {
            var workoutDayId = await this.workoutDayExerciseService.RemoveExerciseFromWorkoutDayAsync(exerciseId);

            return this.RedirectToAction("Edit", "WorkoutDays", new { workoutDayId });
        }
    }
}

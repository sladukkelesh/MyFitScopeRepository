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
        private readonly IWorkoutDaysService workoutDayService;

        public WorkoutDaysExercisesController(IWorkoutDaysExercisesService workoutDayExerciseService, IWorkoutDaysService workoutDayService)
        {
            this.workoutDayExerciseService = workoutDayExerciseService;
            this.workoutDayService = workoutDayService;
        }

        public async Task<IActionResult> AddExerciseToWorkoutDay(AddExerciseToWorkoutDayInputModel input)
        {
            var exerciseAlreadyIn = this.WorkoutDayAlreadyContainsExercise(input.WorkoutDayId, input.ExerciseId);

            if (!string.IsNullOrEmpty(exerciseAlreadyIn))
            {
                this.ModelState.AddModelError(string.Empty, exerciseAlreadyIn);
            }

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Edit", "WorkoutDays", new { workoutDayId = input.WorkoutDayId });
            }

            await this.workoutDayExerciseService.AddExerciseToWorkoutDayAsync(input.ExerciseId, input.WorkoutDayId);

            return this.RedirectToAction("Edit", "WorkoutDays", new { workoutDayId = input.WorkoutDayId });
        }

        public async Task<IActionResult> RemoveExercise(string exerciseId)
        {
            var workoutDayId = await this.workoutDayExerciseService.RemoveExerciseFromWorkoutDayAsync(exerciseId);

            return this.RedirectToAction("Edit", "WorkoutDays", new { workoutDayId });
        }

        private string WorkoutDayAlreadyContainsExercise(string workoutDayId, string exerciseId)
        {
            var workoutDay = this.workoutDayService.GetWorkoutDayById(workoutDayId);

            if (workoutDay.Exercises.Any(e => e.Id == exerciseId))
            {
                var exerciseName = workoutDay.Exercises.FirstOrDefault(e => e.Id == exerciseId).Name;
                var day = workoutDay.WeekDayTitle;

                return day + " already contains exercise with name " + exerciseName;
            }

            return null;
        }
    }
}

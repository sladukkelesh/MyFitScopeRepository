namespace MyFitScope.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Services.Data;
    using MyFitScope.Web.ViewModels.WorkoutDaysExercises;

    public class WorkoutDaysExercisesController : BaseController
    {
        private readonly IWorkoutDaysExercisesService workoutDayExerciseService;
        private readonly IWorkoutDaysService workoutDayService;

        public WorkoutDaysExercisesController(IWorkoutDaysExercisesService workoutDayExerciseService, IWorkoutDaysService workoutDayService)
        {
            this.workoutDayExerciseService = workoutDayExerciseService;
            this.workoutDayService = workoutDayService;
        }

        [Authorize]
        public async Task<IActionResult> AddExerciseToWorkoutDay(AddExerciseToWorkoutDayInputModel input)
        {
            var exerciseAlreadyIn = this.WorkoutDayAlreadyContainsExercise(input.WorkoutDayId, input.ExerciseId);

            // "exerciseAlreadyIn should be BOOL type! add error message in Global constants!!!!!
            if (!string.IsNullOrEmpty(exerciseAlreadyIn))
            {
                this.TempData["error"] = exerciseAlreadyIn;

                return this.RedirectToAction("Edit", "WorkoutDays", new { workoutDayId = input.WorkoutDayId });
            }

            await this.workoutDayExerciseService.AddExerciseToWorkoutDayAsync(input.ExerciseId, input.WorkoutDayId);

            return this.RedirectToAction("Edit", "WorkoutDays", new { workoutDayId = input.WorkoutDayId });
        }

        [Authorize]
        public async Task<IActionResult> RemoveExercise(string exerciseId, string workoutDayId)
        {
            // maybe we have to check if exercise actually exists???
            var workoutDayIdResult = await this.workoutDayExerciseService.RemoveExerciseFromWorkoutDayAsync(exerciseId, workoutDayId);

            return this.RedirectToAction("Edit", "WorkoutDays", new { workoutDayId = workoutDayIdResult });
        }

        [Authorize]
        public async Task<IActionResult> SwapExercisePosition(string currentExerciseId, string targetExerciseId, string workoutDayId)
        {
            // maybe we have to check if current and target exercises actually exists???
            await this.workoutDayExerciseService.SwapExercisesAsync(currentExerciseId, targetExerciseId, workoutDayId);

            return this.RedirectToAction("Edit", "WorkoutDays", new { workoutDayId = workoutDayId });
        }

        public IActionResult Edit(string exerciseId, string workoutDayId)
        {
            var model = this.workoutDayExerciseService.GetExerciseInWorkoutDayById(exerciseId, workoutDayId);

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(EditExercisePropertiesInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.workoutDayExerciseService.EditExercisePropertiesAsync(model.ExerciseId, model.WorkoutDayId, model.Sets, model.Reps, model.Weights, model.Hours, model.Minutes, model.Seconds);

            return this.RedirectToAction("Edit", "WorkoutDays", new { workoutDayId = model.WorkoutDayId });
        }

        private string WorkoutDayAlreadyContainsExercise(string workoutDayId, string exerciseId)
        {
            var workoutDay = this.workoutDayService.GetWorkoutDayById(workoutDayId);

            if (workoutDay.Exercises.Any(e => e.ExerciseId == exerciseId))
            {
                var exerciseName = workoutDay.Exercises.FirstOrDefault(e => e.ExerciseId == exerciseId).ExerciseName;
                var day = workoutDay.WeekDayTitle;

                return day + " already contains exercise with name " + exerciseName;
            }

            return null;
        }
    }
}

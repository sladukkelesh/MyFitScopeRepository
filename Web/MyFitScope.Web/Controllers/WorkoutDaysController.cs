namespace MyFitScope.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Services.Data;
    using MyFitScope.Web.ViewModels.WorkoutDays;

    public class WorkoutDaysController : BaseController
    {
        private const string WeekDayMissing = "Week day field is required!";
        private readonly IWorkoutDaysService workoutDaysService;

        public WorkoutDaysController(IWorkoutDaysService workoutDaysService)
        {
            this.workoutDaysService = workoutDaysService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddWorkoutDay(CreateWorkoutDayInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData["error"] = WeekDayMissing;

                return this.RedirectToAction("CurrentWorkout", "Workouts");
            }

            await this.workoutDaysService.CreateWorkoutDayAsync(input.WorkoutId, input.WeekDay);

            return this.RedirectToAction("CurrentWorkout", "Workouts");
        }

        [Authorize]
        public IActionResult Edit(string workoutDayId)
        {
            var model = this.workoutDaysService.GetWorkoutDayById(workoutDayId);

            return this.View(model);
        }

        [Authorize]
        public async Task<IActionResult> Delete(string workoutDayId)
        {
            await this.workoutDaysService.DeleteWorkoutDayAsync(workoutDayId);

            return this.RedirectToAction("CurrentWorkout", "Workouts");
        }
    }
}

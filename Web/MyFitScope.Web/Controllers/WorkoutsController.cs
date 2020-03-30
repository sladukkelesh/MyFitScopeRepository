namespace MyFitScope.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Data.Models;
    using MyFitScope.Services.Data;
    using MyFitScope.Web.ViewModels.Workouts;

    public class WorkoutsController : Controller
    {
        private readonly IWorkoutsService workoutsService;
        private readonly UserManager<ApplicationUser> userManager;

        public WorkoutsController(IWorkoutsService workoutsService, UserManager<ApplicationUser> userManager)
        {
            this.workoutsService = workoutsService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult AddWorkout()
        {
            var model = new CreateWorkoutInputModel();

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddWorkout(CreateWorkoutInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var userName = this.User.Identity.Name;
            var userId = this.userManager.GetUserId(this.User);

            var workoutId = await this.workoutsService.CreateWorkoutAsync(input.Name, input.Difficulty, input.WorkoutType, input.Description, userName, userId);

            return this.RedirectToAction(nameof(this.CurrentWorkout), new { workoutId = workoutId });
        }

        public IActionResult CurrentWorkout(string workoutId)
        {
            var model = this.workoutsService.GetCurrentWorkout(workoutId);

            return this.View(model);
        }
    }
}

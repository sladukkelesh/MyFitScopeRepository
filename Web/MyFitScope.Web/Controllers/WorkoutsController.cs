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

        public WorkoutsController(SignInManager<ApplicationUser> signInManager, IWorkoutsService workoutsService, UserManager<ApplicationUser> userManager)
        {
            this.workoutsService = workoutsService;
            this.userManager = userManager;
        }

        public ApplicationUser LoggedInUser
            => this.userManager.FindByIdAsync(this.userManager.GetUserId(this.User)).Result;

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

            if (await this.userManager.IsInRoleAsync(this.LoggedInUser, "Admin"))
            {

            }

            var workoutId = await this.workoutsService.CreateWorkoutAsync(input.Name, input.Difficulty, input.WorkoutType, input.Description, this.LoggedInUser.UserName, this.LoggedInUser.Id);

            return this.Redirect("/");
        }

        public async Task<IActionResult> CurrentWorkout()
        {
            var loggInUserId = this.userManager.GetUserId(this.User);
            var loggedInUser = await this.userManager.FindByIdAsync(loggInUserId);

            var model = this.workoutsService.GetCurrentWorkout(loggedInUser.WorkoutId);

            return this.View(model);
        }
    }
}

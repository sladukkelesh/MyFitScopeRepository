namespace MyFitScope.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Ganss.XSS;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Data.Models;
    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Services.Data;
    using MyFitScope.Web.ViewModels.Workouts;

    public class WorkoutsController : BaseController
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

            var loggedInUser = await this.GetLoggedInUserAsync();

            await this.workoutsService.CreateWorkoutAsync(input.Name, input.Difficulty, input.WorkoutType, input.Description, loggedInUser);

            return this.RedirectToAction(nameof(this.CurrentWorkout));
        }

        [Authorize]
        public async Task<IActionResult> CurrentWorkout()
        {
            var loggedInUser = await this.GetLoggedInUserAsync();

            var model = this.workoutsService.GetWorkoutById<CurrentWorkoutViewModel>(loggedInUser.WorkoutId);

            return this.View(model);
        }

        public async Task<IActionResult> WorkoutsListing(string workoutCategory, int? pageIndex = null)
        {
            if (workoutCategory == null)
            {
                workoutCategory = "All";
            }
            else
            {
                workoutCategory = new HtmlSanitizer().Sanitize(workoutCategory);
            }

            var userName = this.User.Identity.Name;
            var isAdmin = this.User.IsInRole("Admin");

            var model = new WorkoutsListingViewModel();

            if (Enum.GetNames(typeof(WorkoutType)).Any(ac => ac == workoutCategory) || workoutCategory == "All" || workoutCategory == "Custom")
            {
                model.Workouts = await this.workoutsService.GetWorkoutsByCategoryAsync(isAdmin, userName, workoutCategory, pageIndex);
                model.WorkoutCategory = "listing=" + workoutCategory;
            }
            else
            {
                model.Workouts = await this.workoutsService.GetWorkoutsByKeyWordAsync(workoutCategory, pageIndex);
                model.WorkoutCategory = "search=" + workoutCategory;
            }

            return this.View(model);
        }

        public IActionResult Details(string workoutId)
        {
            var model = this.workoutsService.GetWorkoutById<DetailsViewModel>(workoutId);

            return this.View(model);
        }

        [Authorize]
        public async Task<IActionResult> SetAsCurrentWorkout(string workoutId)
        {
            var loggedInUser = await this.GetLoggedInUserAsync();

            await this.workoutsService.SetCurrentWorkoutAsync(workoutId, loggedInUser);

            return this.RedirectToAction(nameof(this.CurrentWorkout));
        }

        [Authorize]
        public async Task<IActionResult> DeleteWorkout(string workoutId)
        {
            await this.workoutsService.DeleteWorkoutAsync(workoutId);

            return this.RedirectToAction(nameof(this.WorkoutsListing), new { workoutCategory = "All" });
        }

        [Authorize]
        public IActionResult EditWorkout(string workoutId)
        {
            var model = this.workoutsService.GetWorkoutById<EditWorkoutInputViewModel>(workoutId);

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditWorkout(EditWorkoutInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.workoutsService.EditWorkoutAsync(input.Id, input.Name, input.Difficulty, input.WorkoutType, input.Description);

            return this.RedirectToAction(nameof(this.Details), new { id = input.Id });
        }

        private async Task<ApplicationUser> GetLoggedInUserAsync()
        {
            return await this.userManager.FindByIdAsync(this.userManager.GetUserId(this.User));
        }
    }
}

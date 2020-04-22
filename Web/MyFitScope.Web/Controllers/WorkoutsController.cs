namespace MyFitScope.Web.Controllers
{
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

            var loggedInUser = await this.GetLoggedInUserAsync();

            await this.workoutsService.CreateWorkoutAsync(input.Name, input.Difficulty, input.WorkoutType, input.Description, loggedInUser);

            return this.RedirectToAction(nameof(this.CurrentWorkout));
        }

        public async Task<IActionResult> CurrentWorkout()
        {
            var loggedInUser = await this.GetLoggedInUserAsync();

            var model = this.workoutsService.GetWorkoutById<CurrentWorkoutViewModel>(loggedInUser.WorkoutId);

            return this.View(model);
        }

        public async Task<IActionResult> WorkoutsListing(string workoutCategory, int? pageIndex = null)
        {
            var userName = this.User.Identity.Name;
            var isAdmin = this.User.IsInRole("Admin");

            var model = new WorkoutsListingViewModel
            {
                Workouts = await this.workoutsService.GetWorkoutsByCategoryAsync(isAdmin, userName, workoutCategory, pageIndex),
            };

            model.WorkoutCategory = workoutCategory;

            return this.View(model);
        }

        public IActionResult Details(string id)
        {
            var model = this.workoutsService.GetWorkoutById<DetailsViewModel>(id);

            return this.View(model);
        }

        public async Task<IActionResult> SetAsCurrentWorkout(string workoutId)
        {
            var loggedInUser = await this.GetLoggedInUserAsync();

            await this.workoutsService.SetCurrentWorkoutAsync(workoutId, loggedInUser);

            return this.RedirectToAction(nameof(this.CurrentWorkout));
        }

        public async Task<IActionResult> DeleteWorkout(string workoutId)
        {
            await this.workoutsService.DeleteWorkoutAsync(workoutId);

            return this.RedirectToAction(nameof(this.WorkoutsListing), new { workoutCategory = "All" });
        }

        public IActionResult EditWorkout(string workoutId)
        {
            var model = this.workoutsService.GetWorkoutById<EditWorkoutInputViewModel>(workoutId);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditWorkout(EditWorkoutInputViewModel input)
        {
            await this.workoutsService.EditWorkoutAsync(input.Id, input.Name, input.Difficulty, input.WorkoutType, input.Description);

            return this.RedirectToAction(nameof(this.Details), new { id = input.Id });
        }

        private async Task<ApplicationUser> GetLoggedInUserAsync()
        {
            return await this.userManager.FindByIdAsync(this.userManager.GetUserId(this.User));
        }
    }
}

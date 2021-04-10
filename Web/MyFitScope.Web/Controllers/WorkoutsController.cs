namespace MyFitScope.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Ganss.XSS;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Common;
    using MyFitScope.Data.Models;
    using MyFitScope.Services.Data;
    using MyFitScope.Web.ViewModels.Workouts;

    public class WorkoutsController : BaseController
    {
        private const string ListingPageTitleAll = "All Workouts";
        private const string ListingPageTitleAdminCustom = "Users custom created Workouts";
        private const string ListingPageTitleUserCustom = "{0}'s custom created Workouts";
        private const string ListingPageTitleCategory = "Workouts of category \"{0}\"";
        private const string ListingPageNoResultsMessage = "Sorry, we don't have any workouts in this category at this moment...";

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
            var nameAlreadyExists = this.workoutsService.WorkoutNameAlreadyExists(input.Name);

            if (!this.ModelState.IsValid || nameAlreadyExists)
            {
                if (nameAlreadyExists)
                {
                    this.TempData["error"] = string.Format("Workout with name \"{0}\" already exists! Please, choose a different name!", input.Name);
                }

                return this.View(input);
            }

            var loggedInUser = this.GetLoggedInUserAsync();

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

        public async Task<IActionResult> WorkoutsListing(string workoutsCategory, int? pageIndex = null)
        {
            var userName = this.User.Identity.Name;
            var isAdmin = this.User.IsInRole(GlobalConstants.AdministratorRoleName);

            var model = new WorkoutsListingViewModel
            {
                Workouts = await this.workoutsService.GetWorkoutsByCategoryAsync(isAdmin, userName, workoutsCategory, pageIndex),
                WorkoutsCategory = workoutsCategory,
                PageTitle = this.GetWorkoutsListingPageTitle(workoutsCategory),
                NoResultsMessage = ListingPageNoResultsMessage,
            };

            return this.View(model);
        }

        public async Task<IActionResult> Search(string keyWord, int? pageIndex = null)
        {
            keyWord = new HtmlSanitizer().Sanitize(keyWord);

            var model = new SearchWorkoutsViewModel
            {
                Workouts = await this.workoutsService.GetWorkoutsByKeyWordAsync(keyWord, pageIndex),
                KeyWord = keyWord,
                PageTitle = string.Format(GlobalConstants.SearchPageTitle, keyWord),
                NoResultsMessage = string.Format(GlobalConstants.SearchPageNoResultsMessage, keyWord),
            };

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
            var loggedInUser = await this.GetLoggedInUserAsync();

            await this.workoutsService.DeleteWorkoutAsync(workoutId, loggedInUser.Id);

            return this.RedirectToAction(nameof(this.CurrentWorkout));
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

            return this.RedirectToAction(nameof(this.CurrentWorkout));
        }

        private async Task<ApplicationUser> GetLoggedInUserAsync()
        {
            return await this.userManager.FindByIdAsync(this.userManager.GetUserId(this.User));
        }

        private string GetWorkoutsListingPageTitle(string workoutsCategory)
        {
            var result = string.Empty;

            if (workoutsCategory == "All")
            {
                result = ListingPageTitleAll;
            }

            if (workoutsCategory == "Custom")
            {
                if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
                {
                    result = ListingPageTitleAdminCustom;
                }
                else
                {
                    result = string.Format(ListingPageTitleUserCustom, this.User.Identity.Name);
                }
            }

            if (workoutsCategory != "All" && workoutsCategory != "Custom")
            {
                result = string.Format(ListingPageTitleCategory, workoutsCategory.Replace("_", " "));
            }

            return result;
        }
    }
}

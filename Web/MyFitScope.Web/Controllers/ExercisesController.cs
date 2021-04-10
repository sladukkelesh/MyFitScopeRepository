namespace MyFitScope.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Ganss.XSS;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using MyFitScope.Common;
    using MyFitScope.Data.Models;
    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Services.Data;
    using MyFitScope.Web.ViewModels.Exercises;

    public class ExercisesController : BaseController
    {
        private const string ListingPageTitleAll = "All Exercises";
        private const string ListingPageTitleAdminCustom = "Users custom created Exercises";
        private const string ListingPageTitleUserCustom = "{0}'s custom created Exercises";
        private const string ListingPageTitleCategory = "Exercises of category \"{0}\"";
        private const string ListingPageNoResultsMessage = "Sorry, we don't have any exercises in this category at this moment...";

        private readonly IExercisesService exercisesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWorkoutDaysExercisesService workoutDaysExercisesService;

        public ExercisesController(IExercisesService exercisesService, UserManager<ApplicationUser> userManager, IWorkoutDaysExercisesService workoutDaysExercisesService)
        {
            this.exercisesService = exercisesService;
            this.userManager = userManager;
            this.workoutDaysExercisesService = workoutDaysExercisesService;
        }

        [Authorize]
        public IActionResult CreateExercise()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateExercise(CreateExerciseInputModel input)
        {
            var nameAlreadyExists = this.exercisesService.ExerciseNameAlreadyExists(input.Name);

            if (!this.ModelState.IsValid || nameAlreadyExists)
            {
                if (nameAlreadyExists)
                {
                    this.TempData["error"] = string.Format("Exercise with name \"{0}\" already exists! Please, choose a different name!", input.Name);
                }

                return this.View(input);
            }

            var creatorName = this.User.Identity.Name;
            var isAdmin = this.User.IsInRole(GlobalConstants.AdministratorRoleName);

            await this.exercisesService.CreateExerciseAsync(input.Name, input.VideoUrl, input.MuscleGroup, input.Description, creatorName, isAdmin);

            return this.RedirectToAction(nameof(this.ExercisesListing), new { exerciseCategory = isAdmin ? "All" : "Custom" });
        }

        public async Task<IActionResult> ExercisesListing(string exercisesCategory, int? pageIndex = null)
        {
            var userName = this.User.Identity.Name;
            var isAdmin = this.User.IsInRole(GlobalConstants.AdministratorRoleName);

            var model = new ExerciseListingViewModel
            {
                Exercises = await this.exercisesService.GetExercisesByCategoryAsync(isAdmin, userName, exercisesCategory, pageIndex),
                ExercisesCategory = exercisesCategory,
                PageTitle = this.GetExercisesListingPageTitle(exercisesCategory),
                NoResultsMessage = ListingPageNoResultsMessage,
            };

            return this.View(model);
        }

        public async Task<IActionResult> Search(string keyWord, int? pageIndex = null)
        {
            keyWord = new HtmlSanitizer().Sanitize(keyWord);

            var model = new SearchExercisesViewModel
            {
                Exercises = await this.exercisesService.GetExercisesByKeyWordAsync(keyWord, pageIndex),
                KeyWord = keyWord,
                PageTitle = string.Format(GlobalConstants.SearchPageTitle, keyWord),
                NoResultsMessage = string.Format(GlobalConstants.SearchPageNoResultsMessage, keyWord),
            };

            return this.View(model);
        }

        public IActionResult Details(string id)
        {
            var model = this.exercisesService.GetExerciseById<DetailsExerciseViewModel>(id);

            return this.View(model);
        }

        // This action works for Exercise Manager Muscle Group Menu (left part)
        [Authorize]
        public IActionResult GetMuscleGroups()
        {
            var result = Enum.GetNames(typeof(MuscleGroup))
                        .Select(ac => new ExerciseMuscleGroupOutputModel
                        {
                            GroupName = ac,
                        })
                        .ToList();

            if (!this.User.IsInRole("Admin"))
            {
                result.Add(new ExerciseMuscleGroupOutputModel
                {
                    GroupName = "Custom",
                });
            }

            return this.Ok(result);
        }

        // This action generate the exercise links for every selected muscle group in Exercise Manager Menu
        [Authorize]
        public async Task<IActionResult> GetExercisesByMuscleGroup(string muscleGroup)
        {
            var userName = this.User.Identity.Name;
            var isAdmin = this.User.IsInRole("Admin");

            var exercises = await this.exercisesService.GetExercisesByCategoryAsync(isAdmin, userName, muscleGroup, null);

            var result = exercises.Select(e => new ExerciseOutputModel
            {
                Id = e.Id,
                Name = e.Name,
            });

            return this.Ok(result);
        }

        [Authorize]
        public async Task<IActionResult> DeleteExercise(string exerciseId)
        {
            if (this.workoutDaysExercisesService.GetExerciseConnectionsById(exerciseId).Count() > 0)
            {
                var exerciseConnections = this.workoutDaysExercisesService.GetExerciseConnectionsById(exerciseId);

                foreach (var connection in exerciseConnections)
                {
                    await this.workoutDaysExercisesService.RemoveExerciseFromWorkoutDayAsync(connection.ExerciseId, connection.WorkoutDayId);
                }
            }

            await this.exercisesService.DeleteExerciseAsync(exerciseId);

            return this.RedirectToAction("ExercisesListing");
        }

        [Authorize]
        public IActionResult EditExercise(string exerciseId)
        {
            var model = this.exercisesService.GetExerciseById<EditExerciseInputViewModel>(exerciseId);

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditExercise(EditExerciseInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.exercisesService.EditExerciseAsync(input.Id, input.Name, input.VideoUrl, input.MuscleGroup, input.Description);

            return this.RedirectToAction(nameof(this.Details), new { id = input.Id });
        }

        private string GetExercisesListingPageTitle(string exercisesCategory)
        {
            var result = string.Empty;

            if (exercisesCategory == "All")
            {
                result = ListingPageTitleAll;
            }

            if (exercisesCategory == "Custom")
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

            if (exercisesCategory != "All" && exercisesCategory != "Custom")
            {
                result = string.Format(ListingPageTitleCategory, exercisesCategory);
            }

            return result;
        }
    }
}

namespace MyFitScope.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Data.Models;
    using MyFitScope.Services.Data;
    using MyFitScope.Web.ViewModels.Exercises;

    public class ExercisesController : Controller
    {
        private readonly IExercisesService exercisesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ExercisesController(IExercisesService exercisesService, UserManager<ApplicationUser> userManager)
        {
            this.exercisesService = exercisesService;
            this.userManager = userManager;
        }

        public IActionResult CreateExercise()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateExercise(CreateExerciseInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var creatorName = this.User.Identity.Name;

            await this.exercisesService.CreateExerciseAsync(input.Name, input.VideoUrl, input.MuscleGroup, input.Description, creatorName);

            return this.RedirectToAction("ExercisesListing", new { exerciseCategory = "Custom" });
        }

        public IActionResult ExercisesListing(string exerciseCategory)
        {
            var userName = this.User.Identity.Name;

            var model = new ExerciseListingViewModel
            {
                Exercises = this.exercisesService.GetExercisesByCategory(userName, exerciseCategory),
                ListedGroupType = exerciseCategory ?? "All",
            };

            return this.View(model);
        }

        public IActionResult Details(string id)
        {
            var model = this.exercisesService.GetExerciseById(id);

            return this.View(model);
        }
    }
}

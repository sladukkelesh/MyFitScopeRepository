namespace MyFitScope.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Data.Models;
    using MyFitScope.Services.Data;
    using MyFitScope.Web.ViewModels.Progresses;

    public class ProgressesController : BaseController
    {
        private readonly IProgressesService progressesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProgressesController(IProgressesService progressesService, UserManager<ApplicationUser> userManager)
        {
            this.progressesService = progressesService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Statistics(int? pageIndex = null)
        {
            var model = new StatisticsListingViewModel
            {
                Statistics = await this.progressesService.GetAllStatisticsAsync(pageIndex),
            };

            return this.View(model);
        }

        [Authorize]
        public IActionResult AddStatistic()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddStatistic(CreateStatisticInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            // Uncomment to have the right action behaviour
            // if (this.progressesService.StatisticExists(DateTime.UtcNow))
            // {
            //    this.TempData["error"] = "You can add only 1 Progress Statistic per day!";
            // }
            var userId = this.userManager.GetUserId(this.User);

            await this.progressesService.CreateStatisticAsync(userId, input.Weight, input.Biceps, input.Chest, input.Stomach, input.Hips, input.Thigh, input.Calf);

            return this.RedirectToAction(nameof(this.Statistics));
        }

        [Authorize]
        public async Task<IActionResult> DeleteStatistic(string statisticId)
        {
            await this.progressesService.DeleteStatisticAsync(statisticId);

            return this.RedirectToAction("Statistics");
        }

        public IActionResult EditStatistic(string statisticId)
        {
            var model = this.progressesService.GetById(statisticId);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditStatistic(EditProgressStatisticViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.progressesService.UpdateStatisticAsync(input.Id, input.Weight, input.Biceps, input.Chest, input.Stomach, input.Hips, input.Thigh, input.Calf);

            return this.RedirectToAction(nameof(this.Statistics));
        }
    }
}

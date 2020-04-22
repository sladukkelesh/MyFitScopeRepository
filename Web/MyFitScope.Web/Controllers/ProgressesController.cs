namespace MyFitScope.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Data;
    using MyFitScope.Data.Models;
    using MyFitScope.Services.Data;
    using MyFitScope.Web.ViewModels.Progresses;

    public class ProgressesController : Controller
    {
        private readonly IProgressesService progressesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProgressesController(IProgressesService progressesService, UserManager<ApplicationUser> userManager)
        {
            this.progressesService = progressesService;
            this.userManager = userManager;
        }

        public IActionResult Statistics()
        {
            var model = new StatisticsListingViewModel
            {
                Statistics = this.progressesService.GetAllStatistics(),
            };

            return this.View(model);
        }

        public IActionResult AddStatistic()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStatistic(CreateStatisticInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var userId = this.userManager.GetUserId(this.User);

            await this.progressesService.CreateStatisticAsync(userId, input.Weight, input.Biceps, input.Chest, input.Stomach, input.Hips, input.Thigh, input.Calf);

            return this.RedirectToAction("Statistics");
        }

        public async Task<IActionResult> DeleteStatistic(string statisticId)
        {
            await this.progressesService.DeleteStatisticAsync(statisticId);

            return this.RedirectToAction("Statistics");
        }
    }
}

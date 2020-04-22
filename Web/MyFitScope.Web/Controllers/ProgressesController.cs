namespace MyFitScope.Web.Controllers
{
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
        public IActionResult Statistics()
        {
            var model = new StatisticsListingViewModel
            {
                Statistics = this.progressesService.GetAllStatistics(),
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

            var userId = this.userManager.GetUserId(this.User);

            await this.progressesService.CreateStatisticAsync(userId, input.Weight, input.Biceps, input.Chest, input.Stomach, input.Hips, input.Thigh, input.Calf);

            return this.RedirectToAction("Statistics");
        }

        [Authorize]
        public async Task<IActionResult> DeleteStatistic(string statisticId)
        {
            await this.progressesService.DeleteStatisticAsync(statisticId);

            return this.RedirectToAction("Statistics");
        }
    }
}

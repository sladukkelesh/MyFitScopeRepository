namespace MyFitScope.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Services.Data;
    using MyFitScope.Web.ViewModels.Progresses;

    public class ProgressesController : Controller
    {
        private readonly IProgressesService progressesService;

        public ProgressesController(IProgressesService progressesService)
        {
            this.progressesService = progressesService;
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
            await this.progressesService.CreateStatisticAsync(input.Weight, input.Biceps, input.Chest, input.Stomach, input.Hips, input.Thigh, input.Calf);

            return this.RedirectToAction("Statistics");
        }

        public async Task<IActionResult> DeleteStatistic(string statisticId)
        {
            await this.progressesService.DeleteStatisticAsync(statisticId);

            return this.RedirectToAction("Statistics");
        }
    }
}

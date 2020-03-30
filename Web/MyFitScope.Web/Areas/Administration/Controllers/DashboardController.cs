namespace MyFitScope.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Services.Data;
    using MyFitScope.Web.Controllers;
    using MyFitScope.Web.ViewModels.Administration.Dashboard;

    public class DashboardController : BaseController
    {
        private readonly ISettingsService settingsService;

        public DashboardController(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel { SettingsCount = this.settingsService.GetCount(), };
            return this.View(viewModel);
        }
    }
}

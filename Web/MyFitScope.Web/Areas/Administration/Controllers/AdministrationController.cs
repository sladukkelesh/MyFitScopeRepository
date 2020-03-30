namespace MyFitScope.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Common;
    using MyFitScope.Services.Data;
    using MyFitScope.Web.Controllers;
    using MyFitScope.Web.ViewModels.Administration.Administration;

    //[Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
        private readonly IAdministrationService administrationService;

        public AdministrationController(IAdministrationService administrationService)
        {
            this.administrationService = administrationService;
        }

        public IActionResult AddRole()
        {
            var model = new AddRoleViewModel();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(AddRoleInputViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.administrationService.CreateRoleAsync(model.Name);

            return this.Redirect("/");
        }
    }
}

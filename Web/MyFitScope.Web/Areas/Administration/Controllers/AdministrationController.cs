namespace MyFitScope.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Common;
    using MyFitScope.Data.Models;
    using MyFitScope.Services.Data;
    using MyFitScope.Web.Controllers;
    using MyFitScope.Web.ViewModels.Administration.Administration;

    //[Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAdministrationService administrationService;

        public AdministrationController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, IAdministrationService administrationService)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
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

        public IActionResult AllRoles()
        {
            var model = new AllRolesViewModel
            {
                Roles = this.administrationService.GetAllRoles(),
            };

            return this.View(model);
        }

        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            var role = await this.roleManager.FindByIdAsync(roleId);

            var model = await this.administrationService.ListUsersInRoleAsync(roleId, role.Name);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(IEnumerable<UsersInRoleViewModel> users, string roleId)
        {
            await this.administrationService.UpdateUsersInRoleAsync(users, roleId);

            return this.RedirectToAction("AllRoles");
        }
    }
}

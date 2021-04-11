namespace MyFitScope.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using MyFitScope.Common;
    using MyFitScope.Data.Models;
    using MyFitScope.Web.ViewModels.Administration.Administration;

    public class AdministrationService : IAdministrationService
    {
        private const string InvalidRoleIdErrorMessage = "Role with ID: {0} does not exist.";
        private const string InvalidUserIdErrorMessage = "User with ID: {0} does not exist.";

        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public AdministrationService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task CreateRoleAsync(string name)
        {
            var role = new ApplicationRole
            {
                Name = name,
            };

            await this.roleManager.CreateAsync(role);
        }

        public IEnumerable<RoleViewModel> GetAllRoles()
            => this.roleManager
                            .Roles
                            .Select(r => new RoleViewModel
                            {
                                Id = r.Id,
                                Name = r.Name,
                            })
                            .OrderBy(r => r.Name)
                            .ToList();

        public async Task<IEnumerable<UsersInRoleViewModel>> ListUsersInRoleAsync(string roleId, string roleName)
        {
            var result = new List<UsersInRoleViewModel>();

            foreach (var user in this.userManager.Users.ToList())
            {
                var model = new UsersInRoleViewModel();

                model.RoleId = roleId;
                model.UserId = user.Id;
                model.UserName = user.UserName;

                if (await this.userManager.IsInRoleAsync(user, roleName))
                {
                    model.IsSelected = true;
                }
                else
                {
                    model.IsSelected = false;
                }

                result.Add(model);
            }

            return result;
        }

        public async Task UpdateUsersInRoleAsync(IEnumerable<UsersInRoleViewModel> users, string roleId)
        {
            var role = await this.roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                throw new ArgumentNullException(
                    string.Format(InvalidRoleIdErrorMessage, roleId));
            }

            foreach (var user in users)
            {
                var userObj = await this.userManager.FindByIdAsync(user.UserId);

                if (userObj == null)
                {
                    throw new ArgumentNullException(
                    string.Format(InvalidUserIdErrorMessage, user.UserId));
                }

                if (user.IsSelected && !(await this.userManager.IsInRoleAsync(userObj, role.Name)))
                {
                    await this.userManager.AddToRoleAsync(userObj, role.Name);
                    await this.userManager.RemoveFromRoleAsync(userObj, role.Name == GlobalConstants.AdministratorRoleName ? GlobalConstants.UserRoleName : GlobalConstants.AdministratorRoleName);
                }
                else if (!user.IsSelected && (await this.userManager.IsInRoleAsync(userObj, role.Name)))
                {
                    await this.userManager.RemoveFromRoleAsync(userObj, role.Name);
                    await this.userManager.AddToRoleAsync(userObj, role.Name == GlobalConstants.AdministratorRoleName ? GlobalConstants.UserRoleName : GlobalConstants.AdministratorRoleName);
                }
            }
        }
    }
}

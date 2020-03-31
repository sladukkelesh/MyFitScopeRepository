namespace MyFitScope.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyFitScope.Web.ViewModels.Administration.Administration;

    public interface IAdministrationService
    {
        Task CreateRoleAsync(string name);

        IEnumerable<RoleViewModel> GetAllRoles();

        Task<IEnumerable<UsersInRoleViewModel>> ListUsersInRoleAsync(string roleId, string roleName);

        Task UpdateUsersInRoleAsync(IEnumerable<UsersInRoleViewModel> users, string roleId);
    }
}

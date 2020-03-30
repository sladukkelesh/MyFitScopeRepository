namespace MyFitScope.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using MyFitScope.Data.Common.Repositories;
    using MyFitScope.Data.Models;

    public class AdministrationService : IAdministrationService
    {
        private readonly RoleManager<ApplicationRole> roleManager;

        public AdministrationService(RoleManager<ApplicationRole> roleManager)
        {
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
    }
}

namespace MyFitScope.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using MyFitScope.Common;
    using MyFitScope.Data.Models;

    internal class UsersSeeder : ISeeder
    {
        // Creates one "admin" (pass: adminpass) and two regular users (pass: user{2 or 3}pass):
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any())
            {
                return;
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            for (int i = 1; i <= GlobalConstants.UsersEntitiesCount; i++)
            {
                var userName = (i > 1) ? $"user{i}" : "admin";

                var newUser = new ApplicationUser()
                {
                    CreatedOn = DateTime.Now,
                    UserName = $"{userName}@gmail.bg",
                    Email = $"{userName}@gmail.bg",
                };

                var result = await userManager.CreateAsync(newUser, $"{userName}pass");

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
                else
                {
                    await userManager.AddToRoleAsync(newUser, (i > 1) ? GlobalConstants.UserRoleName : GlobalConstants.AdministratorRoleName);
                }
            }
        }
    }
}

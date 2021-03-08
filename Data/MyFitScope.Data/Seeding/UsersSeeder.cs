namespace MyFitScope.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using MyFitScope.Data.Models;

    internal class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any())
            {
                return;
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            for (int i = 1; i <= 3; i++)
            {
                var newUser = new ApplicationUser()
                {
                    CreatedOn = DateTime.Now,
                    UserName = $"user{i}@abv.bg",
                    Email = $"user{i}@abv.bg",
                };

                var result = await userManager.CreateAsync(newUser, $"user{i}pass");

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
                else
                {
                    await userManager.AddToRoleAsync(newUser, "Administrator");
                }
            }
        }
    }
}

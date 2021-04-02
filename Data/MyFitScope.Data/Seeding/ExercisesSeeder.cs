namespace MyFitScope.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using MyFitScope.Common;
    using MyFitScope.Data.Models;
    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Data.Models.FitnessModels.Enums;

    public class ExercisesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Exercises.Any())
            {
                return;
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var admin = (await userManager.GetUsersInRoleAsync(GlobalConstants.AdministratorRoleName)).FirstOrDefault();

            for (int i = 1; i <= GlobalConstants.ExercisesEntitiesCount; i++)
            {
                var exerciseName = $"{Enum.GetName(typeof(MuscleGroup), i)} Exercise".Replace("_", " ");

                var exercise = new Exercise
                {
                    Name = exerciseName,
                    IsCustom = false,
                    CreatorName = admin.UserName,
                    VideoUrl = GlobalConstants.ExerciseVideoUrl,
                    MuscleGroup = (MuscleGroup)i,
                    Description = GlobalConstants.ExerciseDescription,
                };

                dbContext.Exercises.Add(exercise);
            }
        }
    }
}

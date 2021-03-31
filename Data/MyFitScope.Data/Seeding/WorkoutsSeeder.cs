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

    public class WorkoutsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var admin = (await userManager.GetUsersInRoleAsync(GlobalConstants.AdministratorRoleName)).FirstOrDefault();

            // Random values from "Dificulty" enum:
            var rand = new Random();
            var randomMax = Convert.ToInt32(Enum.GetValues(typeof(Difficulty))
                                                .Cast<Difficulty>()
                                                .Max());
            var randomMin = Convert.ToInt32(Enum.GetValues(typeof(Difficulty))
                                                .Cast<Difficulty>()
                                                .Min());

            for (int i = 1; i <= GlobalConstants.WorkoutsEntitiesCount; i++)
            {
                var workout = new Workout
                {
                    Name = $"{Enum.GetName(typeof(WorkoutType), i).Replace("_", " ")}",
                    CreatorName = admin.UserName,
                    IsCustom = false,
                    Difficulty = (Difficulty)rand.Next(randomMin, randomMax + 1),
                    WorkoutType = (WorkoutType)i,
                    Description = GlobalConstants.WorkoutDescription,
                };

                this.AddWorkoutDaysToWorkout(dbContext, workout);

                dbContext.Workouts.Add(workout);
            }
        }

        private void AddWorkoutDaysToWorkout(ApplicationDbContext dbContext, Workout workout)
        {
            for (int i = 1; i <= 5; i += 2)
            {
                var workoutDay = new WorkoutDay
                {
                    WeekDay = (WeekDay)i, // "i" will correspond to "Monday"(1), "Wednessday"(3) and "Friday"(5) from "WeekDay" enum!
                    Workout = workout,
                };

                this.AddExercisesToWorkoutDay(dbContext, workoutDay);

                dbContext.WorkoutDays.Add(workoutDay);

                workout.WorkoutDays.Add(workoutDay);
            }
        }

        private void AddExercisesToWorkoutDay(ApplicationDbContext dbContext, WorkoutDay workoutday)
        {
            var exercises = dbContext.Exercises.OrderBy(e => Guid.NewGuid()).Take(5).ToList();

            for (int i = 0; i < 5; i++)
            {
                var workoutDayExercise = new WorkoutDayExercise
                {
                    WorkoutDay = workoutday,
                    ExerciseId = exercises[i].Id,
                };

                dbContext.WorkoutDaysExercises.Add(workoutDayExercise);
            }
        }
    }
}

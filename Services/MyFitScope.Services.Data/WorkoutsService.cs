namespace MyFitScope.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using MyFitScope.Data.Common.Repositories;
    using MyFitScope.Data.Models;
    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Services.Mapping;
    using MyFitScope.Web.ViewModels.Workouts;

    public class WorkoutsService : IWorkoutsService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<Workout> workoutsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public WorkoutsService(UserManager<ApplicationUser> userManager, IDeletableEntityRepository<Workout> workoutsRepository, IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.userManager = userManager;
            this.workoutsRepository = workoutsRepository;
            this.usersRepository = usersRepository;
        }

        public async Task<string> CreateWorkoutAsync(string name, Difficulty difficulty, WorkoutType workoutType, string description, ApplicationUser user)
        {
            var workout = new Workout
            {
                Name = name,
                Difficulty = difficulty,
                WorkoutType = workoutType,
                Description = description,
                CreatorName = user.UserName,
            };

            if (await this.userManager.IsInRoleAsync(user, "Admin"))
            {
                workout.IsCustom = false;
            }
            else
            {
                workout.IsCustom = true;
            }

            workout.Users.Add(await this.usersRepository.GetByIdWithDeletedAsync(user.Id));

            await this.workoutsRepository.AddAsync(workout);
            await this.workoutsRepository.SaveChangesAsync();

            return workout.Id;
        }

        public T GetWorkoutById<T>(string workoutId)
            => this.workoutsRepository.All()
                .Where(w => w.Id == workoutId)
                .To<T>()
                .FirstOrDefault();

        public ICollection<WorkoutViewModel> GetWorkoutsByCategory(string userName, string workoutCategory)
        {
            var workouts = this.workoutsRepository.All();

            if (workoutCategory != null)
            {
                if (workoutCategory == "Custom")
                {
                    workouts = workouts.Where(w => w.IsCustom == true && w.CreatorName == userName);
                }
                else
                {
                    workouts = workouts.Where(w => w.WorkoutType == (WorkoutType)Enum.Parse(typeof(WorkoutType), workoutCategory) && w.IsCustom == false);
                }
            }
            else
            {
                workouts = workouts.Where(w => w.IsCustom == false);
            }

            return workouts.OrderByDescending(w => w.CreatedOn).To<WorkoutViewModel>().ToList();
        }
    }
}

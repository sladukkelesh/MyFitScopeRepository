namespace MyFitScope.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using MyFitScope.Common;
    using MyFitScope.Data.Common.Repositories;
    using MyFitScope.Data.Models;
    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Services.Mapping;
    using MyFitScope.Web.Infrastructure;
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

        public async Task CreateWorkoutAsync(string name, Difficulty difficulty, WorkoutType workoutType, string description, ApplicationUser user)
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
        }

        public async Task DeleteWorkoutAsync(string workoutId)
        {
            var workoutToDelete = await this.workoutsRepository.GetByIdWithDeletedAsync(workoutId);

            workoutToDelete.IsDeleted = true;

            await this.workoutsRepository.SaveChangesAsync();
        }

        public async Task EditWorkoutAsync(string workoutId, string name, Difficulty difficulty, WorkoutType workoutType, string description)
        {
            var workoutToEdit = await this.workoutsRepository.GetByIdWithDeletedAsync(workoutId);

            workoutToEdit.Name = name;
            workoutToEdit.Difficulty = difficulty;
            workoutToEdit.WorkoutType = workoutType;
            workoutToEdit.Description = description;
            workoutToEdit.ModifiedOn = DateTime.UtcNow;

            this.workoutsRepository.Update(workoutToEdit);
            await this.workoutsRepository.SaveChangesAsync();
        }

        public T GetWorkoutById<T>(string workoutId)
            => this.workoutsRepository.All()
                .Where(w => w.Id == workoutId)
                .To<T>()
                .FirstOrDefault();

        public async Task<PaginatedList<WorkoutViewModel>> GetWorkoutsByCategoryAsync(bool isAdmin, string userName, string workoutCategory, int? pageIndex = null)
        {
            var workouts = this.workoutsRepository.All();

            if (workoutCategory != null && workoutCategory != "All")
            {
                if (workoutCategory == "Custom")
                {
                    if (isAdmin)
                    {
                        workouts = workouts.Where(w => w.IsCustom == true);
                    }
                    else
                    {
                        workouts = workouts.Where(w => w.IsCustom == true && w.CreatorName == userName);
                    }
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

            return await PaginatedList<WorkoutViewModel>.CreateAsync(workouts.OrderByDescending(w => w.CreatedOn).To<WorkoutViewModel>(), pageIndex ?? GlobalConstants.PaginationDefaultPageIndex, GlobalConstants.PaginationPageSize);
        }

        public async Task SetCurrentWorkoutAsync(string workoutId, ApplicationUser user)
        {
            user.WorkoutId = workoutId;

            await this.usersRepository.SaveChangesAsync();
        }
    }
}

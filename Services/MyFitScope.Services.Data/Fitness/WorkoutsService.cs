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
        private const string InvalidWorkoutIdErrorMessage = "Workout with ID: {0} does not exist.";

        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<Workout> workoutsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IUsersService usersService;

        public WorkoutsService(UserManager<ApplicationUser> userManager, IDeletableEntityRepository<Workout> workoutsRepository, IDeletableEntityRepository<ApplicationUser> usersRepository, IUsersService usersService)
        {
            this.userManager = userManager;
            this.workoutsRepository = workoutsRepository;
            this.usersRepository = usersRepository;
            this.usersService = usersService;
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

        public async Task DeleteWorkoutAsync(string workoutId, string userId)
        {
            var workoutToDelete = await this.workoutsRepository.GetByIdWithDeletedAsync(workoutId);

            if (workoutToDelete == null)
            {
                throw new ArgumentNullException(
                    string.Format(InvalidWorkoutIdErrorMessage, workoutId));
            }

            await this.usersService.RemoveWorkoutFromUserAsync(userId);

            workoutToDelete.IsDeleted = true;
            await this.workoutsRepository.SaveChangesAsync();
        }

        public async Task EditWorkoutAsync(string workoutId, string name, Difficulty difficulty, WorkoutType workoutType, string description)
        {
            var workoutToEdit = await this.workoutsRepository.GetByIdWithDeletedAsync(workoutId);

            if (workoutToEdit == null)
            {
                throw new ArgumentNullException(
                    string.Format(InvalidWorkoutIdErrorMessage, workoutId));
            }

            workoutToEdit.Name = name;
            workoutToEdit.Difficulty = difficulty;
            workoutToEdit.WorkoutType = workoutType;
            workoutToEdit.Description = description;
            workoutToEdit.ModifiedOn = DateTime.UtcNow;

            this.workoutsRepository.Update(workoutToEdit);
            await this.workoutsRepository.SaveChangesAsync();
        }

        public T GetWorkoutById<T>(string workoutId)
        {
            var workout = this.workoutsRepository.All()
                                .Where(w => w.Id == workoutId)
                                .To<T>()
                                .FirstOrDefault();
            return workout;
        }

        public async Task<PaginatedList<WorkoutViewModel>> GetWorkoutsByCategoryAsync(bool isAdmin, string userName, string workoutsCategory, int? pageIndex = null)
        {
            var workouts = this.workoutsRepository.All();

            if (workoutsCategory != "All")
            {
                if (workoutsCategory == "Custom")
                {
                    if (isAdmin)
                    {
                        // if User is in role "Admin", select all custom workouts:
                        workouts = workouts.Where(w => w.IsCustom == true);
                    }
                    else
                    {
                        // select only the custom workouts for the current User:
                        workouts = workouts.Where(w => w.IsCustom == true && w.CreatorName == userName);
                    }
                }
                else
                {
                    // select workouts by chosen category:
                    workouts = workouts.Where(w => w.WorkoutType == (WorkoutType)Enum.Parse(typeof(WorkoutType), workoutsCategory) && w.IsCustom == false);
                }
            }
            else
            {
                // select all workouts wich are not custom created:
                workouts = workouts.Where(w => w.IsCustom == false);
            }

            return await PaginatedList<WorkoutViewModel>.CreateAsync(workouts.OrderByDescending(w => w.CreatedOn).To<WorkoutViewModel>(), pageIndex ?? GlobalConstants.PaginationDefaultPageIndex, GlobalConstants.PaginationPageSize);
        }

        public async Task<PaginatedList<WorkoutViewModel>> GetWorkoutsByKeyWordAsync(string keyWord, int? pageIndex)
        {
            var result = this.workoutsRepository.All()
                             .Where(e => e.Name.ToLower().Contains(keyWord.ToLower())
                             || e.Description.ToLower().Contains(keyWord.ToLower()));

            return await PaginatedList<WorkoutViewModel>.CreateAsync(result.OrderByDescending(r => r.CreatedOn).To<WorkoutViewModel>(), pageIndex ?? GlobalConstants.PaginationDefaultPageIndex, GlobalConstants.PaginationPageSize);
        }

        public async Task SetCurrentWorkoutAsync(string workoutId, ApplicationUser user)
        {
            user.WorkoutId = workoutId;

            await this.usersRepository.SaveChangesAsync();
        }

        public bool WorkoutNameAlreadyExists(string name)
        {
            return this.workoutsRepository.All().Any(e => e.Name == name);
        }
    }
}

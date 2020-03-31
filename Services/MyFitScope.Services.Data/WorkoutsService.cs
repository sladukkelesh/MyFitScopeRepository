namespace MyFitScope.Services.Data
{
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

        public CurrentWorkoutViewModel GetCurrentWorkout(string workoutId)
            => this.workoutsRepository.All()
                .Where(w => w.Id == workoutId)
                .To<CurrentWorkoutViewModel>()
                .FirstOrDefault();
    }
}

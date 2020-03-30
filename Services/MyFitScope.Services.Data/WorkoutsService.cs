namespace MyFitScope.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using MyFitScope.Data.Common.Repositories;
    using MyFitScope.Data.Models;
    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Services.Mapping;
    using MyFitScope.Web.ViewModels.Workouts;

    public class WorkoutsService : IWorkoutsService
    {
        private readonly IDeletableEntityRepository<Workout> workoutsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public WorkoutsService(IDeletableEntityRepository<Workout> workoutsRepository, IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.workoutsRepository = workoutsRepository;
            this.usersRepository = usersRepository;
        }

        public async Task<string> CreateWorkoutAsync(string name, Difficulty difficulty, WorkoutType workoutType, string description, string creatorName, string userId)
        {
            var workout = new Workout
            {
                Name = name,
                Difficulty = difficulty,
                WorkoutType = workoutType,
                Description = description,
                CreatorName = creatorName,
            };

            workout.Users.Add(await this.usersRepository.GetByIdWithDeletedAsync(userId));

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

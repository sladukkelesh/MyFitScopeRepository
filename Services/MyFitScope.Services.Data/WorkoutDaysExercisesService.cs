namespace MyFitScope.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using MyFitScope.Data.Common.Repositories;
    using MyFitScope.Data.Models.FitnessModels;

    public class WorkoutDaysExercisesService : IWorkoutDaysExercisesService
    {
        private readonly IRepository<WorkoutDayExercise> workoutDayExerciseRepository;

        public WorkoutDaysExercisesService(IRepository<WorkoutDayExercise> workoutDayExerciseRepository)
        {
            this.workoutDayExerciseRepository = workoutDayExerciseRepository;
        }

        public async Task AddExerciseToWorkoutDayAsync(string exerciseId, string workoutDayId)
        {
            var workoutDayExercise = new WorkoutDayExercise
            {
                ExerciseId = exerciseId,
                WorkoutDayId = workoutDayId,
            };

            await this.workoutDayExerciseRepository.AddAsync(workoutDayExercise);
            await this.workoutDayExerciseRepository.SaveChangesAsync();
        }

        public async Task<string> RemoveExerciseFromWorkoutDayAsync(string exerciseId, string workoutDayId)
        {
            var targetToDelete = this.workoutDayExerciseRepository.All()
                                     .Where(we => we.ExerciseId == exerciseId && we.WorkoutDayId == workoutDayId)
                                     .FirstOrDefault();

            this.workoutDayExerciseRepository.Delete(targetToDelete);
            await this.workoutDayExerciseRepository.SaveChangesAsync();

            return targetToDelete.WorkoutDayId;
        }
    }
}

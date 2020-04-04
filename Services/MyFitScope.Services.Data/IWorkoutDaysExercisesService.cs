namespace MyFitScope.Services.Data
{
    using System.Threading.Tasks;

    public interface IWorkoutDaysExercisesService
    {
        Task AddExerciseToWorkoutDayAsync(string exerciseId, string workoutDayId);

        Task<string> RemoveExerciseFromWorkoutDayAsync(string exerciseId);
    }
}

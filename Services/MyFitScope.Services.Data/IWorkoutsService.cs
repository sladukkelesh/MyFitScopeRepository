namespace MyFitScope.Services.Data
{
    using System.Threading.Tasks;

    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Web.ViewModels.Workouts;

    public interface IWorkoutsService
    {
        Task<string> CreateWorkoutAsync(string name, Difficulty difficulty, WorkoutType workoutType, string description, string creatorName, string userId);

        CurrentWorkoutViewModel GetCurrentWorkout(string workoutId);
    }
}

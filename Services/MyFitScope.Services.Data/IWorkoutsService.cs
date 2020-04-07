namespace MyFitScope.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyFitScope.Data.Models;
    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Web.ViewModels.Workouts;

    public interface IWorkoutsService
    {
        Task<string> CreateWorkoutAsync(string name, Difficulty difficulty, WorkoutType workoutType, string description, ApplicationUser user);

        ICollection<WorkoutViewModel> GetWorkoutsByCategory(string userName, string workoutCategory);

        T GetWorkoutById<T>(string workoutId);
    }
}

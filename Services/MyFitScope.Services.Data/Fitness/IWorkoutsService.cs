namespace MyFitScope.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyFitScope.Data.Models;
    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Web.Infrastructure;
    using MyFitScope.Web.ViewModels.Workouts;

    public interface IWorkoutsService
    {
        Task CreateWorkoutAsync(string name, Difficulty difficulty, WorkoutType workoutType, string description, ApplicationUser user);

        Task<PaginatedList<WorkoutViewModel>> GetWorkoutsByCategoryAsync(bool isAdmin, string userName, string workoutCategory, int? pageIndex);

        T GetWorkoutById<T>(string workoutId);

        Task SetCurrentWorkoutAsync(string workoutId, ApplicationUser user);

        Task DeleteWorkoutAsync(string workoutId);

        Task EditWorkoutAsync(string workoutId, string name, Difficulty difficulty, WorkoutType workoutType, string description);

        Task<PaginatedList<WorkoutViewModel>> GetWorkoutsByKeyWordAsync(string keyWord, int? pageIndex);

        bool WorkoutNameAlreadyExists(string name);
    }
}

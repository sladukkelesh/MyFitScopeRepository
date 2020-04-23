namespace MyFitScope.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyFitScope.Web.ViewModels.WorkoutDaysExercises;

    public interface IWorkoutDaysExercisesService
    {
        Task AddExerciseToWorkoutDayAsync(string exerciseId, string workoutDayId);

        Task<string> RemoveExerciseFromWorkoutDayAsync(string exerciseId, string workoutDayId);

        IEnumerable<WorkoutDaysExercisesOutputModel> GetByExerciseId(string exerciseId);
    }
}

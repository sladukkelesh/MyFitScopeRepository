namespace MyFitScope.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyFitScope.Web.ViewModels.WorkoutDaysExercises;

    public interface IWorkoutDaysExercisesService
    {
        Task AddExerciseToWorkoutDayAsync(string exerciseId, string workoutDayId);

        Task SwapExercisesAsync(string currentExerciseId, string targetExerciseId, string workoutDayId);

        Task<string> RemoveExerciseFromWorkoutDayAsync(string exerciseId, string workoutDayId);

        Task EditExercisePropertiesAsync(string currentExerciseId, string currentWorkoutDayId, int sets, int reps, double weights, int hours, int minutes, int seconds);

        IEnumerable<WorkoutDaysExercisesOutputModel> GetExerciseConnectionsById(string exerciseId);

        EditExercisePropertiesInputModel GetExerciseInWorkoutDayById(string exerciseId, string workoutDayId);
    }
}

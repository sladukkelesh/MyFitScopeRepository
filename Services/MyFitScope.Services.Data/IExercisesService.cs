namespace MyFitScope.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Web.ViewModels.Exercises;

    public interface IExercisesService
    {
        Task CreateExerciseAsync(string name, string videoUrl, MuscleGroup muscleGroup, string description, string creatorName);

        Task<IEnumerable<ExerciseViewModel>> GetExercisesByCategoryAsync(string userName, string exerciseCategory, bool withPagination, int? pageIndex);

        T GetExerciseById<T>(string exerciseId);

        Task DeleteExerciseAsync(string exerciseId);

        Task EditExerciseAsync(string exerciseId, string name, string videoUrl, MuscleGroup muscleGroup, string description);
    }
}

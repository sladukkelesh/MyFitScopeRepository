namespace MyFitScope.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Web.ViewModels.Exercises;

    public interface IExercisesService
    {
        Task CreateExerciseAsync(string name, string videoUrl, MuscleGroup muscleGroup, string description, string creatorName);

        ICollection<ExerciseViewModel> GetExercisesByCategory(string userName, string exerciseCategory);

        DetailsExerciseViewModel GetExerciseById(string exerciseId);

        Task DeleteExerciseAsync(string exerciseId);
    }
}

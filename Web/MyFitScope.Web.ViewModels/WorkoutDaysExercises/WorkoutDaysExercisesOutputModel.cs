namespace MyFitScope.Web.ViewModels.WorkoutDaysExercises
{
    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Services.Mapping;

    public class WorkoutDaysExercisesOutputModel : IMapFrom<WorkoutDayExercise>
    {
        public string ExerciseId { get; set; }

        public string WorkoutDayId { get; set; }
    }
}

namespace MyFitScope.Web.ViewModels.WorkoutDays
{
    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Services.Mapping;

    public class EditWorkoutDayExerciseViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public MuscleGroup MuscleGroup { get; set; }

        public string MuscleGroupTitle
            => this.MuscleGroup.ToString().Replace("_", " ");
    }
}

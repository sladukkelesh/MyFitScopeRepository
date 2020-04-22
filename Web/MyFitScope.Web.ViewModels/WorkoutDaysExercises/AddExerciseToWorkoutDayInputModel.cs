namespace MyFitScope.Web.ViewModels.WorkoutDaysExercises
{
    using System.ComponentModel.DataAnnotations;

    public class AddExerciseToWorkoutDayInputModel
    {
        [Required]
        public string ExerciseId { get; set; }

        [Required]
        public string WorkoutDayId { get; set; }
    }
}

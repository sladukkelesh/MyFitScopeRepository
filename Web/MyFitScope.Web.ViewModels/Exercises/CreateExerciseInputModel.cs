namespace MyFitScope.Web.ViewModels.Exercises
{
    using System.ComponentModel.DataAnnotations;

    using MyFitScope.Data.Models.FitnessModels.Enums;

    public class CreateExerciseInputModel
    {
        public ExerciseSettings ExerciseSettings { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(30)]
        public string Name { get; set; }

        public string VideoUrl { get; set; }

        public MuscleGroup MuscleGroup { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(1000)]
        public string Description { get; set; }
    }
}

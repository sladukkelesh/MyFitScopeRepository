namespace MyFitScope.Web.ViewModels.Workouts
{
    using System.ComponentModel.DataAnnotations;

    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Services.Mapping;

    public class CreateWorkoutInputModel
    {
        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public string Name { get; set; }

        [Required]
        public Difficulty Difficulty { get; set; }

        [Required]
        public WorkoutType WorkoutType { get; set; }

        [Required]
        [MaxLength(2000)]
        [MinLength(10)]
        public string Description { get; set; }
    }
}

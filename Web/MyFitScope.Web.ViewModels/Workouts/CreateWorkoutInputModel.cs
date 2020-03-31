namespace MyFitScope.Web.ViewModels.Workouts
{
    using System.ComponentModel.DataAnnotations;

    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Services.Mapping;

    public class CreateWorkoutInputModel : IMapFrom<Workout>
    {
        [Required]
        [MaxLength(50)]
        [MinLength(5)]
        public string Name { get; set; }

        public Difficulty Difficulty { get; set; }

        public WorkoutType WorkoutType { get; set; }

        public bool IsCustom { get; set; }

        [Required]
        [MaxLength(2000)]
        [MinLength(10)]
        public string Description { get; set; }
    }
}

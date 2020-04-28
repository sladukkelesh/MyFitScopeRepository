namespace MyFitScope.Web.ViewModels.Workouts
{
    using System.ComponentModel.DataAnnotations;

    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Services.Mapping;

    public class EditWorkoutInputViewModel : IMapFrom<Workout>
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot be greater than 50 symbols!")]
        [MinLength(5, ErrorMessage = "Name cannot be smaller than 5 symbols!")]
        public string Name { get; set; }

        [Required]
        public Difficulty Difficulty { get; set; }

        [Required]
        public WorkoutType WorkoutType { get; set; }

        public bool IsCustom { get; set; }

        [Required]
        [MaxLength(2000, ErrorMessage = "Description cannot be greater than 2000 symbols!")]
        [MinLength(10, ErrorMessage = "Description cannot be smaller than 10 symbols!")]
        public string Description { get; set; }
    }
}

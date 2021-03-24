namespace MyFitScope.Web.ViewModels.Exercises
{
    using System.ComponentModel.DataAnnotations;

    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Services.Mapping;
    using MyFitScope.Web.Infrastructure.ValidationAttributes;

    public class EditExerciseInputViewModel : IMapFrom<Exercise>
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Name cannot be smaller than 5 symbols!")]
        [MaxLength(50, ErrorMessage = "Name cannot be greater than 50 symbols!")]
        public string Name { get; set; }

        [YouTubeUrlValidation]
        [Display(Name = "Video Url")]
        public string VideoUrl { get; set; }

        [Required]
        [Display(Name = "Muscle Group")]
        public MuscleGroup MuscleGroup { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Description cannot be smaller than 5 symbols!")]
        [MaxLength(2000, ErrorMessage = "Desription cannot be greater than 2000 symbols!")]
        public string Description { get; set; }
    }
}

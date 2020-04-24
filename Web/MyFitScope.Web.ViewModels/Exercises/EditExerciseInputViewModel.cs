namespace MyFitScope.Web.ViewModels.Exercises
{
    using System.ComponentModel.DataAnnotations;

    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Services.Mapping;

    public class EditExerciseInputViewModel : IMapFrom<Exercise>
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^(http(s)?:\/\/)?((w){3}.)?youtu(be|.be)?(\.com)?\/.+", ErrorMessage = "Video Url must be a valid YouTube Url!")]
        [Display(Name = "Video Url")]
        public string VideoUrl { get; set; }

        [Required]
        [Display(Name = "Muscle Group")]
        public MuscleGroup MuscleGroup { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(1000)]
        public string Description { get; set; }
    }
}

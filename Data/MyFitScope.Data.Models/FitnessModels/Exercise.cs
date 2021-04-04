namespace MyFitScope.Data.Models.FitnessModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyFitScope.Data.Common.Models;
    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Data.Models.ValidationAttributes;

    public class Exercise : BaseDeletableModel<string>
    {
        public Exercise()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
            this.WorkoutDays = new HashSet<WorkoutDayExercise>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public bool IsCustom { get; set; }

        [Required]
        [MaxLength(50)]
        public string CreatorName { get; set; }

        [YouTubeUrlValidation]
        public string VideoUrl { get; set; }

        [Required]
        public MuscleGroup MuscleGroup { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        public ICollection<WorkoutDayExercise> WorkoutDays { get; set; }
    }
}

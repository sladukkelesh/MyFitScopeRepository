namespace MyFitScope.Data.Models.FitnessModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MyFitScope.Data.Common.Models;
    using MyFitScope.Data.Models.FitnessModels.Enums;

    public class Workout : BaseDeletableModel<string>
    {
        public Workout()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
            this.IsCustom = false;
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string CreatorName { get; set; }

        public bool IsCustom { get; set; }

        public Difficulty Difficulty { get; set; }

        public WorkoutType WorkoutType { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }
    }
}

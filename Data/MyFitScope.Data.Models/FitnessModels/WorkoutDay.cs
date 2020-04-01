namespace MyFitScope.Data.Models.FitnessModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MyFitScope.Data.Common.Models;
    using MyFitScope.Data.Models.FitnessModels.Enums;

    public class WorkoutDay : BaseDeletableModel<string>
    {
        public WorkoutDay()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; } // Auto generated!

        [Required]
        public WeekDay WeekDay { get; set; }

        [Required]
        public string WorkoutId { get; set; }

        public Workout Workout { get; set; }
    }
}

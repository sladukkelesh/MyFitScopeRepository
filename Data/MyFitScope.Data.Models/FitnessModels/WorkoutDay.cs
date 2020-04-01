namespace MyFitScope.Data.Models.FitnessModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MyFitScope.Data.Common.Models;

    public class WorkoutDay : BaseDeletableModel<string>
    {
        public WorkoutDay()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; } // Auto generated!

        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        [Required]
        public int WorkoutId { get; set; }

        public Workout Workout { get; set; }
    }
}

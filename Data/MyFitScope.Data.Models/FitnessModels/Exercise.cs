namespace MyFitScope.Data.Models.FitnessModels
{
    using System;
    using System.Collections.Generic;

    using MyFitScope.Data.Common.Models;
    using MyFitScope.Data.Models.FitnessModels.Enums;

    public class Exercise : BaseDeletableModel<string>
    {
        public Exercise()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
            this.WorkoutDays = new HashSet<WorkoutDayExercise>();
        }

        public ExerciseSettings ExerciseSettings { get; set; }

        public string Name { get; set; }

        public bool IsCustom { get; set; }

        public string CreatorName { get; set; }

        public string VideoUrl { get; set; } //-------> Not required!

        public MuscleGroup MuscleGroup { get; set; }

        public string Description { get; set; } //--------> Required!

        public ICollection<WorkoutDayExercise> WorkoutDays { get; set; }
    }
}

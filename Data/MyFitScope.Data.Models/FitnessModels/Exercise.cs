namespace MyFitScope.Data.Models.FitnessModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
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

        public string VideoUrl { get; set; }

        public MuscleGroup MuscleGroup { get; set; }

        public string Description { get; set; }

        public ICollection<WorkoutDayExercise> WorkoutDays { get; set; }
    }
}

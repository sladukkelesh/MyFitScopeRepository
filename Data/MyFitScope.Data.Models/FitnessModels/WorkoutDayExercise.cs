namespace MyFitScope.Data.Models.FitnessModels
{
    using System;

    using MyFitScope.Data.Common.Models;

    public class WorkoutDayExercise : BaseModel<int>
    {
        public WorkoutDayExercise()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        public string WorkoutDayId { get; set; }

        public virtual WorkoutDay WorkoutDay { get; set; }

        public string ExerciseId { get; set; }

        public virtual Exercise Exercise { get; set; }
    }
}

namespace MyFitScope.Data.Models.FitnessModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MyFitScope.Data.Common.Models;

    public class WorkoutDayExercise : BaseModel<int>
    {
        public WorkoutDayExercise()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.TimeInterval = new TimeSpan(0, 0, 0);
        }

        public string WorkoutDayId { get; set; }

        public virtual WorkoutDay WorkoutDay { get; set; }

        public string ExerciseId { get; set; }

        public virtual Exercise Exercise { get; set; }

        // Property "Position" --> keep the position of exercise in workoutDay exercises collection!
        public int Position { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Number of Sets must be 0 or greater!")]
        public int Sets { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Number of Reps must be 0 or greater!")]
        public int Reps { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "Weights value must be 0 or greater!")]
        public double Weights { get; set; }

        [DataType(DataType.Time)]
        [Range(typeof(TimeSpan), "00:00:00", "23:59:59", ErrorMessage = "Time Interval value must be between 00:00:00 and 23:59:59!")]
        public TimeSpan TimeInterval { get; set; }
    }
}

namespace MyFitScope.Web.ViewModels.WorkoutDays
{
    using System;

    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Services.Mapping;

    public class EditWorkoutDayExerciseViewModel : IMapFrom<WorkoutDayExercise>
    {
        public DateTime CreatedOn { get; set; }

        public string ExerciseId { get; set; }

        public string ExerciseName { get; set; }

        public int Position { get; set; }

        public MuscleGroup ExerciseMuscleGroup { get; set; }

        public string MuscleGroupTitle
            => this.ExerciseMuscleGroup.ToString().Replace("_", " ");

        public string WorkoutDayId { get; set; }
    }
}

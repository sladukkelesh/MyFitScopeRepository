﻿namespace MyFitScope.Web.ViewModels.WorkoutDays
{
    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Services.Mapping;

    public class WorkoutDayExerciseViewModel : IMapFrom<WorkoutDayExercise>
    {
        public string ExerciseId { get; set; }

        public string ExerciseName { get; set; }

        public MuscleGroup ExerciseMuscleGroup { get; set; }

        public string MuscleGroupTitle
            => this.ExerciseMuscleGroup.ToString().Replace("_", " ");
    }
}
namespace MyFitScope.Web.ViewModels.Workouts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Services.Mapping;

    public class CurrentWorkoutViewModel : IMapFrom<Workout>
    {
        public string Name { get; set; }

        public string CreatorName { get; set; }

        public bool IsCustom { get; set; }

        public Difficulty Difficulty { get; set; }

        public string DifficultyTitle
            => this.Difficulty.ToString().Replace("_", " ");

        public WorkoutType WorkoutType { get; set; }

        public string WorkoutTypeTitle
            => this.WorkoutType.ToString().Replace("_", " ");

        public string Description { get; set; }
    }
}

namespace MyFitScope.Web.ViewModels.WorkoutDays
{
    using System.Collections.Generic;

    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Services.Mapping;

    public class EditWorkoutDayViewModel
    {
        public string Id { get; set; }

        public WeekDay WeekDay { get; set; }

        public string WeekDayTitle
            => this.WeekDay.ToString();

        public List<EditWorkoutDayExerciseViewModel> Exercises { get; set; }
    }
}

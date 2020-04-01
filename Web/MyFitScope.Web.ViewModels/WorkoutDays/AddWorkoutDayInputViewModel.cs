namespace MyFitScope.Web.ViewModels.WorkoutDays
{
    using MyFitScope.Data.Models.FitnessModels.Enums;

    public class AddWorkoutDayInputViewModel
    {
        public WeekDay WeekDay { get; set; }

        public string WorkoutId { get; set; }
    }
}

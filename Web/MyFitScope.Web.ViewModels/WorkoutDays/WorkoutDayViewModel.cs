namespace MyFitScope.Web.ViewModels.WorkoutDays
{
    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Services.Mapping;

    public class WorkoutDayViewModel : IMapFrom<WorkoutDay>
    {
        public WeekDay WeekDay { get; set; }

        public string WeekDayTitle
            => this.WeekDay.ToString();
    }
}

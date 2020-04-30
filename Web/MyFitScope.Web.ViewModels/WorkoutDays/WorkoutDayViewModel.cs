namespace MyFitScope.Web.ViewModels.WorkoutDays
{
    using System.Collections.Generic;
    using System.Linq;
    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Services.Mapping;

    public class WorkoutDayViewModel : IMapFrom<WorkoutDay>
    {
        public string Id { get; set; }

        public string Name
        {
            get
            {
                var muscleGroups = this.Exercises.Select(e => e.MuscleGroupTitle).Distinct().OrderBy(x => x).ToList();

                return string.Join(", ", muscleGroups);
            }
        }

        public WeekDay WeekDay { get; set; }

        public string WeekDayTitle
            => this.WeekDay.ToString();

        public List<WorkoutDayExerciseViewModel> Exercises { get; set; }
    }
}

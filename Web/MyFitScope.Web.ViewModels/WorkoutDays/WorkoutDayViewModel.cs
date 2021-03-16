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

        public string MuscleGroupsNames
        {
            get
            {
                var muscleGroups = this.Exercises.OrderBy(e => e.Position).Select(e => e.MuscleGroupTitle).Distinct().ToList();

                return string.Join(", ", muscleGroups);
            }
        }

        public string WorkoutDayTitle
        {
            get
            {

                return this.WeekDayName + (this.MuscleGroupsNames != string.Empty ? " - " + this.MuscleGroupsNames : string.Empty);
            }
        }

        public string WeekDayName
        {
            get
            {
                return this.WeekDay.ToString();
            }
        }

        public WeekDay WeekDay { get; set; }

        public List<WorkoutDayExerciseViewModel> Exercises { get; set; }
    }
}

namespace MyFitScope.Web.ViewModels.Workouts
{
    using System.Collections.Generic;

    using Ganss.XSS;
    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Services.Mapping;
    using MyFitScope.Web.ViewModels.WorkoutDays;

    public class DetailsViewModel : IMapFrom<Workout>
    {
        public string Id { get; set; }

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

        public string SanitizedDescription
            => new HtmlSanitizer().Sanitize(this.Description);

        public ICollection<WorkoutDayViewModel> WorkoutDays { get; set; }
    }
}

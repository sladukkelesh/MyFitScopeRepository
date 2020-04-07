namespace MyFitScope.Web.ViewModels.Workouts
{
    using System.Net;
    using System.Text.RegularExpressions;

    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Services.Mapping;

    public class WorkoutViewModel : IMapFrom<Workout>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CreatorName { get; set; }

        public string Description { get; set; }

        public string ShortDescription
        {
            get
            {
                var description = WebUtility.HtmlDecode(Regex.Replace(this.Description, @"<[^>]+>", string.Empty));

                return description.Length > 200 ? description.Substring(0, 200) + "..." : description;
            }
        }

        public WorkoutType WorkoutType { get; set; }

        public string WorkoutTypeTitle
            => this.WorkoutType.ToString().Replace("_", " ");

        public Difficulty Difficulty { get; set; }

        public string DifficultyTitle
            => this.Difficulty.ToString();
    }
}

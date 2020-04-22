namespace MyFitScope.Web.ViewModels.Exercises
{
    using System;
    using System.Net;
    using System.Text.RegularExpressions;

    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Services.Mapping;

    public class ExerciseViewModel : IMapFrom<Exercise>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CreatorName { get; set; }

        public string VideoUrl { get; set; }

        public string ThumbnailUrl
            => "https://img.youtube.com/vi/" + this.VideoUrl.Split("=", StringSplitOptions.RemoveEmptyEntries)[1] + "/sddefault.jpg";

        public MuscleGroup MuscleGroup { get; set; }

        public string MuscleGroupTitle
            => this.MuscleGroup.ToString().Replace("_", " ");

        public string Description { get; set; }

        public string ShortDescription
        {
            get
            {
                var description = WebUtility.HtmlDecode(Regex.Replace(this.Description, @"<[^>]+>", string.Empty));

                return description.Length > 200 ? description.Substring(0, 200) + "..." : description;
            }
        }
    }
}

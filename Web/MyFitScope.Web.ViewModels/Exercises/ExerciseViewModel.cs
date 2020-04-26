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

        public DateTime CreatedOn { get; set; }

        public string VideoUrl { get; set; }

        public string ThumbnailUrl
        {
            get
            {
                if (this.VideoUrl != null)
                {
                    return "https://img.youtube.com/vi/" + this.VideoUrl.Split("=", StringSplitOptions.RemoveEmptyEntries)[1] + "/sddefault.jpg";
                }

                return "https://w7.pngwing.com/pngs/712/530/png-transparent-silhouette-illustration-of-weight-lifter-fitness-centre-computer-icons-physical-exercise-expander-fitness-trainer-fitness-room-gym-gymnastic-health-512-angle-white-physical-fitness-thumbnail.png";
            }
        }

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

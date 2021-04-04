namespace MyFitScope.Web.ViewModels.Exercises
{
    using System;

    using Ganss.XSS;

    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Services.Mapping;

    public class DetailsExerciseViewModel : IMapFrom<Exercise>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CreatorName { get; set; }

        public string VideoUrl { get; set; }

        public string EmbededVideoUrl
        {
            get
            {
                if (this.VideoUrl != null)
                {
                    return "https://www.youtube.com/embed/" + this.VideoUrl.Split("=", StringSplitOptions.RemoveEmptyEntries)[1];
                }

                return null;
            }
        }

        public MuscleGroup MuscleGroup { get; set; }

        public string MuscleGroupTitle
            => this.MuscleGroup.ToString().Replace("_", " ");

        public string Description { get; set; }

        public string SanitizedDescription
            => new HtmlSanitizer().Sanitize(this.Description);
    }
}

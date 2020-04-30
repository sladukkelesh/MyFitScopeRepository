namespace MyFitScope.Web.ViewModels.ProgressImages
{
    using System;
    using System.Globalization;
    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Services.Mapping;

    public class ProgressImageViewModel : IMapFrom<ProgressImage>
    {
        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedOnString
            => this.CreatedOn.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

        public string Url { get; set; }
    }
}
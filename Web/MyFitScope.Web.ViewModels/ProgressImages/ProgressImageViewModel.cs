namespace MyFitScope.Web.ViewModels.ProgressImages
{
    using System;

    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Services.Mapping;

    public class ProgressImageViewModel : IMapFrom<ProgressImage>
    {
        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Url { get; set; }
    }
}
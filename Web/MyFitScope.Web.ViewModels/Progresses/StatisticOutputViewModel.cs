namespace MyFitScope.Web.ViewModels.Progresses
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Services.Mapping;

    public class StatisticOutputViewModel : IMapFrom<Progress>
    {
        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public double Weight { get; set; }

        public double? Biceps { get; set; }

        public double? Chest { get; set; }

        public double? Stomach { get; set; }

        public double? Hips { get; set; }

        public double? Thigh { get; set; }

        public double? Calf { get; set; }
    }
}
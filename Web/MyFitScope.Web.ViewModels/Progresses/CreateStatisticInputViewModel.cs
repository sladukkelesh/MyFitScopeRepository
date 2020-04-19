namespace MyFitScope.Web.ViewModels.Progresses
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateStatisticInputViewModel
    {
        [Required]
        public double Weight { get; set; }

        public double? Biceps { get; set; }

        public double? Chest { get; set; }

        public double? Stomach { get; set; }

        public double? Hips { get; set; }

        public double? Thigh { get; set; }

        public double? Calf { get; set; }

        public IFormFile ProgressPhoto { get; set; }
    }
}

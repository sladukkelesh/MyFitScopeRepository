﻿namespace MyFitScope.Web.ViewModels.Progresses
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateStatisticInputViewModel
    {
        [Required]
        [Range(20, double.MaxValue, ErrorMessage = "The Weight value must be greater than 20kg!")]
        public double Weight { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "The Biceps value must be a positive number!")]
        public double? Biceps { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "The Chest value must be a positive number")]
        public double? Chest { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "The Stomach value must be a positive number")]
        public double? Stomach { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "The Hips value must be a positive number")]
        public double? Hips { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "The Thigh value must be a positive number")]
        public double? Thigh { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "The Calf value must be a positive number")]
        public double? Calf { get; set; }
    }
}

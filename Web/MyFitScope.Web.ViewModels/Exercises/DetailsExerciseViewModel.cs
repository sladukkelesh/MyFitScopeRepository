﻿namespace MyFitScope.Web.ViewModels.Exercises
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Services.Mapping;

    public class DetailsExerciseViewModel : IMapFrom<Exercise>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string VideoUrl { get; set; }

        public string EmbededVideoUrl
            => "https://www.youtube.com/embed/" + this.VideoUrl.Split("=", StringSplitOptions.RemoveEmptyEntries)[1];

        public MuscleGroup MuscleGroup { get; set; }

        public string MuscleGroupTitle
            => this.MuscleGroup.ToString().Replace("_", " ");

        public string Description { get; set; }
    }
}

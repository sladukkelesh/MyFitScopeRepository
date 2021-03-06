﻿namespace MyFitScope.Data.Models.FitnessModels.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum MuscleGroup
    {
        Abs = 1,
        Back = 2,
        Biceps = 3,
        Chest = 4,
        Forearms = 5,
        Glutes = 6,
        Shoulders = 7,
        Triceps = 8,

        [Display(Name = "Upper Legs")]
        Upper_Legs = 9,

        [Display(Name ="Lower Legs")]
        Lower_Legs = 10,

        Cardio = 11,
    }
}

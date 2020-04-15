namespace MyFitScope.Data.Models.FitnessModels.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum ExerciseSettings
    {
        [Display(Name = "Weight And Reps")]
        Weight_And_Reps = 1,

        [Display(Name = "Reps Only")]
        Reps_Only = 2,

        [Display(Name = "Time Only")]
        Time_Only = 3,
    }
}

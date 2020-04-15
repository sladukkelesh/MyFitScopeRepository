namespace MyFitScope.Data.Models.FitnessModels.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum WorkoutType
    {
        Gym = 1,

        [Display(Name = "Home Workout")]
        Home_Workout = 2,

        Cardio = 3,
        CrossFit = 4,
        Custom = 5,
    }
}

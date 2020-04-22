namespace MyFitScope.Web.ViewModels.WorkoutDays
{
    using System.ComponentModel.DataAnnotations;

    using MyFitScope.Data.Models.FitnessModels.Enums;

    public class CreateWorkoutDayInputViewModel
    {
        [Required]
        public string WorkoutId { get; set; }

        [Required]
        public WeekDay WeekDay { get; set; }
    }
}

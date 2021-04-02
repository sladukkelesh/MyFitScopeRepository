namespace MyFitScope.Web.ViewModels.WorkoutDaysExercises
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Services.Mapping;

    public class EditExercisePropertiesInputModel : IMapFrom<WorkoutDayExercise>, IValidatableObject
    {
        public string WorkoutDayId { get; set; }

        public string ExerciseId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Number of Sets must be 0 or greater!")]
        public int Sets { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Number of Reps must be 0 or greater!")]
        public int Reps { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "Weights value must be 0 or greater!")]
        public double Weights { get; set; }

        [Range(0, 59, ErrorMessage = "Hours must be between 0 and 59!")]
        public int Hours { get; set; }

        [Range(0, 59, ErrorMessage = "Minutes must be between 0 and 59!")]
        public int Minutes { get; set; }

        [Range(0, 59, ErrorMessage = "Seconds must be between 0 and 59!")]
        public int Seconds { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if ((this.Sets == 0 && this.Reps == 0 && this.Weights == 0)
                && (this.Hours == 0 && this.Minutes == 0 && this.Seconds == 0))
            {
                yield return new ValidationResult("Atleast one of the values --> Sets, Reps, Weights, Time Interval parameters --> must be different than 0!");
            }
        }
    }
}

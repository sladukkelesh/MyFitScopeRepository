namespace MyFitScope.Web.ViewModels.WorkoutDays
{
    using System;

    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Services.Mapping;

    public class WorkoutDayExerciseViewModel : IMapFrom<WorkoutDayExercise>
    {
        public int Id { get; set; }

        public string ExerciseId { get; set; }

        public string ExerciseName { get; set; }

        public string ExerciseVideoUrl { get; set; }

        public string ThumbnailUrl
        {
            get
            {
                if (this.ExerciseVideoUrl != null)
                {
                    return "https://img.youtube.com/vi/" + this.ExerciseVideoUrl.Split("=", StringSplitOptions.RemoveEmptyEntries)[1] + "/sddefault.jpg";
                }

                return "https://w7.pngwing.com/pngs/712/530/png-transparent-silhouette-illustration-of-weight-lifter-fitness-centre-computer-icons-physical-exercise-expander-fitness-trainer-fitness-room-gym-gymnastic-health-512-angle-white-physical-fitness-thumbnail.png";
            }
        }

        public int Position { get; set; }

        public MuscleGroup ExerciseMuscleGroup { get; set; }

        public string MuscleGroupTitle
            => this.ExerciseMuscleGroup.ToString().Replace("_", " ");
    }
}
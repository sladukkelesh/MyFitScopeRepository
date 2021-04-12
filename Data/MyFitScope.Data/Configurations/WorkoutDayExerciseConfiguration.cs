namespace MyFitScope.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using MyFitScope.Data.Models.FitnessModels;

    public class WorkoutDayExerciseConfiguration : IEntityTypeConfiguration<WorkoutDayExercise>
    {
        public void Configure(EntityTypeBuilder<WorkoutDayExercise> workoutDayExercise)
        {
            workoutDayExercise
                .HasKey(wde => new { wde.WorkoutDayId, wde.ExerciseId });

            workoutDayExercise
                .HasOne(wde => wde.WorkoutDay)
                .WithMany(wd => wd.Exercises)
                .HasForeignKey(wde => wde.WorkoutDayId)
                .OnDelete(DeleteBehavior.Restrict);

            workoutDayExercise
                .HasOne(wde => wde.Exercise)
                .WithMany(e => e.WorkoutDays)
                .HasForeignKey(wde => wde.ExerciseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

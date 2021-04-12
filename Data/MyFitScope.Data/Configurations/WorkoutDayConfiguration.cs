namespace MyFitScope.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using MyFitScope.Data.Models.FitnessModels;

    public class WorkoutDayConfiguration : IEntityTypeConfiguration<WorkoutDay>
    {
        public void Configure(EntityTypeBuilder<WorkoutDay> workoutDay)
        {
            workoutDay
                .HasOne(wd => wd.Workout)
                .WithMany(w => w.WorkoutDays)
                .HasForeignKey(wd => wd.WorkoutId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

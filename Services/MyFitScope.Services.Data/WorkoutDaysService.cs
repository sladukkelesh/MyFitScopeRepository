namespace MyFitScope.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MyFitScope.Data.Common.Repositories;
    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Services.Mapping;
    using MyFitScope.Web.ViewModels.WorkoutDays;

    public class WorkoutDaysService : IWorkoutDaysService
    {
        private readonly IDeletableEntityRepository<WorkoutDay> workoutDaysRepository;

        public WorkoutDaysService(IDeletableEntityRepository<WorkoutDay> workoutDaysRepository)
        {
            this.workoutDaysRepository = workoutDaysRepository;
        }

        public async Task CreateWorkoutDayAsync(string workoutId, WeekDay weekDay)
        {
            var workoutDay = new WorkoutDay
            {
                WeekDay = weekDay,
                WorkoutId = workoutId,
            };

            await this.workoutDaysRepository.AddAsync(workoutDay);
            await this.workoutDaysRepository.SaveChangesAsync();
        }

        public EditWorkoutDayViewModel GetWorkoutDayById(string workoutDayId)
            => this.workoutDaysRepository.All()
                                 .Where(wd => wd.Id == workoutDayId)
                                 .Select(wd => new EditWorkoutDayViewModel
                                 {
                                     Id = wd.Id,
                                     WeekDay = wd.WeekDay,
                                     Exercises = wd.Exercises.Select(e => new EditWorkoutDayExerciseViewModel
                                     {
                                         Id = e.ExerciseId,
                                         Name = e.Exercise.Name,
                                         MuscleGroup = e.Exercise.MuscleGroup,
                                     })
                                     .ToList(),
                                 })
                                 .FirstOrDefault();
    }
}

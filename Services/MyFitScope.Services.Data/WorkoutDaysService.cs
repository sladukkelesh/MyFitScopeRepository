namespace MyFitScope.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using MyFitScope.Data.Common.Repositories;
    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Data.Models.FitnessModels.Enums;

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
    }
}

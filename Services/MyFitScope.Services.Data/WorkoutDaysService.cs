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
    using MyFitScope.Web.ViewModels.WorkoutDaysExercises;

    public class WorkoutDaysService : IWorkoutDaysService
    {
        private readonly IDeletableEntityRepository<WorkoutDay> workoutDaysRepository;
        private readonly IWorkoutDaysExercisesService workoutDaysExercisesService;
        private readonly IRepository<WorkoutDayExercise> workoutDaysExercisesRespository;

        public WorkoutDaysService(IDeletableEntityRepository<WorkoutDay> workoutDaysRepository, IWorkoutDaysExercisesService workoutDaysExercisesService, IRepository<WorkoutDayExercise> workoutDaysExercisesRespository)
        {
            this.workoutDaysRepository = workoutDaysRepository;
            this.workoutDaysExercisesService = workoutDaysExercisesService;
            this.workoutDaysExercisesRespository = workoutDaysExercisesRespository;
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

        public async Task DeleteWorkoutDayAsync(string workoutDayId)
        {
            var workoutDayToDelete = await this.workoutDaysRepository.GetByIdWithDeletedAsync(workoutDayId);

            if (this.workoutDaysExercisesRespository.All().Any(we => we.WorkoutDayId == workoutDayId))
            {
                var exercisesConnectionsToDelete = this.workoutDaysExercisesRespository.All()
                                                   .Where(we => we.WorkoutDayId == workoutDayId)
                                                   .To<WorkoutDaysExercisesOutputModel>()
                                                   .ToList();

                foreach (var connection in exercisesConnectionsToDelete)
                {
                    await this.workoutDaysExercisesService.RemoveExerciseFromWorkoutDayAsync(connection.ExerciseId, connection.WorkoutDayId);
                }
            }

            this.workoutDaysRepository.HardDelete(workoutDayToDelete);
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

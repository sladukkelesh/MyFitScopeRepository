namespace MyFitScope.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyFitScope.Data.Common.Repositories;
    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Services.Mapping;
    using MyFitScope.Web.ViewModels.WorkoutDaysExercises;

    public class WorkoutDaysExercisesService : IWorkoutDaysExercisesService
    {
        private const string InvalidExerciseConnectionIdErrorMessage = "Exercise connection with exerciseID: {0}  and workoutID: {1} does not exist.";

        private readonly IRepository<WorkoutDayExercise> workoutDayExerciseRepository;

        public WorkoutDaysExercisesService(IRepository<WorkoutDayExercise> workoutDayExerciseRepository)
        {
            this.workoutDayExerciseRepository = workoutDayExerciseRepository;
        }

        public async Task AddExerciseToWorkoutDayAsync(string exerciseId, string workoutDayId)
        {
            var workoutDayExercise = new WorkoutDayExercise
            {
                ExerciseId = exerciseId,
                WorkoutDayId = workoutDayId,
                Position = this.GenerateExercisePosition(workoutDayId),
            };

            await this.workoutDayExerciseRepository.AddAsync(workoutDayExercise);
            await this.workoutDayExerciseRepository.SaveChangesAsync();
        }

        public async Task SwapExercisesAsync(string currentExerciseId, string targetExerciseId, string workoutDayId)
        {
            var currentExercise = this.workoutDayExerciseRepository
                                      .All()
                                      .Where(e => e.ExerciseId == currentExerciseId && e.WorkoutDayId == workoutDayId)
                                      .FirstOrDefault();

            var targetExerise = this.workoutDayExerciseRepository
                                    .All()
                                    .Where(e => e.ExerciseId == targetExerciseId && e.WorkoutDayId == workoutDayId)
                                    .FirstOrDefault();

            var temp = currentExercise.Position;
            currentExercise.Position = targetExerise.Position;
            targetExerise.Position = temp;

            await this.workoutDayExerciseRepository.SaveChangesAsync();
        }

        public async Task<string> RemoveExerciseFromWorkoutDayAsync(string exerciseId, string workoutDayId)
        {
            var targetToDelete = this.workoutDayExerciseRepository.All()
                                     .Where(we => we.ExerciseId == exerciseId && we.WorkoutDayId == workoutDayId)
                                     .FirstOrDefault();

            if (targetToDelete == null)
            {
                throw new ArgumentNullException(
                    string.Format(InvalidExerciseConnectionIdErrorMessage, exerciseId, workoutDayId));
            }

            this.UpdateExercisesPositions(targetToDelete.Position, workoutDayId);

            this.workoutDayExerciseRepository.Delete(targetToDelete);
            await this.workoutDayExerciseRepository.SaveChangesAsync();

            return targetToDelete.WorkoutDayId;
        }

        public IEnumerable<WorkoutDaysExercisesOutputModel> GetByExerciseId(string exerciseId)
            => this.workoutDayExerciseRepository.All()
                                                .Where(we => we.ExerciseId == exerciseId)
                                                .To<WorkoutDaysExercisesOutputModel>()
                                                .ToList();

        private int GenerateExercisePosition(string workoutDayId)
            => this.workoutDayExerciseRepository.All()
                                                .Where(wd => wd.WorkoutDayId == workoutDayId)
                                                .Count() + 1;

        private void UpdateExercisesPositions(int exercisePosition, string workoutDayId)
        {
            var exercises = this.workoutDayExerciseRepository.All()
                                .Where(wde => wde.WorkoutDayId == workoutDayId)
                                .OrderBy(e => e.Position)
                                .ToList();

            for (int i = exercisePosition; i < exercises.Count; i++)
            {
                exercises[i].Position--;
            }
        }
    }
}

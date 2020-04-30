﻿namespace MyFitScope.Services.Data
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
            };

            await this.workoutDayExerciseRepository.AddAsync(workoutDayExercise);
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

            this.workoutDayExerciseRepository.Delete(targetToDelete);
            await this.workoutDayExerciseRepository.SaveChangesAsync();

            return targetToDelete.WorkoutDayId;
        }

        public IEnumerable<WorkoutDaysExercisesOutputModel> GetByExerciseId(string exerciseId)
        {
            return this.workoutDayExerciseRepository.All()
                                                    .Where(we => we.ExerciseId == exerciseId)
                                                    .To<WorkoutDaysExercisesOutputModel>()
                                                    .ToList();
        }

    }
}
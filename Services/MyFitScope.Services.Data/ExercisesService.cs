namespace MyFitScope.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MyFitScope.Common;
    using MyFitScope.Data.Common.Repositories;
    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Services.Mapping;
    using MyFitScope.Web.Infrastructure;
    using MyFitScope.Web.ViewModels.Exercises;

    public class ExercisesService : IExercisesService
    {
        private readonly IDeletableEntityRepository<Exercise> exercisesRepository;

        public ExercisesService(IDeletableEntityRepository<Exercise> exercisesRepository)
        {
            this.exercisesRepository = exercisesRepository;
        }

        public async Task CreateExerciseAsync(string name, string videoUrl, MuscleGroup muscleGroup, string description, string creatorName)
        {
            var exercise = new Exercise
            {
                Name = name,
                CreatedOn = DateTime.UtcNow,
                VideoUrl = videoUrl,
                MuscleGroup = muscleGroup,
                Description = description,
                CreatorName = creatorName,
            };

            await this.exercisesRepository.AddAsync(exercise);
            await this.exercisesRepository.SaveChangesAsync();
        }

        public async Task DeleteExerciseAsync(string exerciseId)
        {
            var exerciseToDelete = await this.exercisesRepository.GetByIdWithDeletedAsync(exerciseId);

            exerciseToDelete.IsDeleted = true;

            await this.exercisesRepository.SaveChangesAsync();
        }

        public async Task EditExerciseAsync(string exerciseId, string name, string videoUrl, MuscleGroup muscleGroup, string description)
        {
            var exerciseToEdit = await this.exercisesRepository.GetByIdWithDeletedAsync(exerciseId);

            exerciseToEdit.Name = name;
            exerciseToEdit.VideoUrl = videoUrl;
            exerciseToEdit.MuscleGroup = muscleGroup;
            exerciseToEdit.Description = description;
            exerciseToEdit.ModifiedOn = DateTime.UtcNow;

            this.exercisesRepository.Update(exerciseToEdit);
            await this.exercisesRepository.SaveChangesAsync();
        }

        public T GetExerciseById<T>(string exerciseId)
             => this.exercisesRepository.All()
                    .Where(e => e.Id == exerciseId)
                    .To<T>()
                    .FirstOrDefault();

        public async Task<IEnumerable<ExerciseViewModel>> GetExercisesByCategoryAsync(string userName, string exerciseCategoryInput, bool withPagination, int? pageIndex = null)
        {
            var result = this.exercisesRepository.All();

            if (exerciseCategoryInput != null && exerciseCategoryInput != "All")
            {
                if (exerciseCategoryInput == "Custom")
                {
                    // select only custom exercises for loggedIn User
                    result = result.Where(e => e.CreatorName == userName);
                }
                else
                {
                    // select exercises by chosen category
                    result = result.Where(e => e.MuscleGroup == (MuscleGroup)Enum.Parse(typeof(MuscleGroup), exerciseCategoryInput) && e.IsCustom == false);
                }
            }
            else
            {
                // select all exercises wich are not custom created
                result = result.Where(e => e.IsCustom == false);
            }

            if (withPagination)
            {
                return await PaginatedList<ExerciseViewModel>.CreateAsync(result.To<ExerciseViewModel>(), pageIndex ?? GlobalConstants.PaginationDefaultPageIndex, GlobalConstants.PaginationPageSize);
            }

            return result.To<ExerciseViewModel>().ToList();
        }

        public async Task<PaginatedList<ExerciseViewModel>> GetExercisesByKeyWordAsync(string keyWord, int? pageIndex)
        {
            var result = this.exercisesRepository.All()
                             .Where(e => e.Name.Contains(keyWord));

            return await PaginatedList<ExerciseViewModel>.CreateAsync(result.To<ExerciseViewModel>(), pageIndex ?? GlobalConstants.PaginationDefaultPageIndex, GlobalConstants.PaginationPageSize);
        }
    }
}

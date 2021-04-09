namespace MyFitScope.Services.Data
{
    using System;
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
        private const string InvalidExerciseIdErrorMessage = "Exercise with ID: {0} does not exist.";

        private readonly IDeletableEntityRepository<Exercise> exercisesRepository;

        public ExercisesService(IDeletableEntityRepository<Exercise> exercisesRepository)
        {
            this.exercisesRepository = exercisesRepository;
        }

        public async Task CreateExerciseAsync(string name, string videoUrl, MuscleGroup muscleGroup, string description, string creatorName, bool isAdmin)
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

            if (isAdmin)
            {
                exercise.IsCustom = false;
            }
            else
            {
                exercise.IsCustom = true;
            }

            await this.exercisesRepository.AddAsync(exercise);
            await this.exercisesRepository.SaveChangesAsync();
        }

        public async Task DeleteExerciseAsync(string exerciseId)
        {
            var exerciseToDelete = await this.exercisesRepository.GetByIdWithDeletedAsync(exerciseId);

            if (exerciseToDelete == null)
            {
                throw new ArgumentNullException(
                    string.Format(InvalidExerciseIdErrorMessage, exerciseId));
            }

            exerciseToDelete.IsDeleted = true;

            await this.exercisesRepository.SaveChangesAsync();
        }

        public async Task EditExerciseAsync(string exerciseId, string name, string videoUrl, MuscleGroup muscleGroup, string description)
        {
            var exerciseToEdit = await this.exercisesRepository.GetByIdWithDeletedAsync(exerciseId);

            if (exerciseToEdit == null)
            {
                throw new ArgumentNullException(
                    string.Format(InvalidExerciseIdErrorMessage, exerciseId));
            }

            exerciseToEdit.Name = name;
            exerciseToEdit.VideoUrl = videoUrl;
            exerciseToEdit.MuscleGroup = muscleGroup;
            exerciseToEdit.Description = description;
            exerciseToEdit.ModifiedOn = DateTime.UtcNow;

            this.exercisesRepository.Update(exerciseToEdit);
            await this.exercisesRepository.SaveChangesAsync();
        }

        public bool ExerciseNameAlreadyExists(string name)
        {
            return this.exercisesRepository.All().Any(e => e.Name == name);
        }

        public T GetExerciseById<T>(string exerciseId)
        {
            var exercise = this.exercisesRepository.All()
                                .Where(e => e.Id == exerciseId)
                                .To<T>()
                                .FirstOrDefault();

            if (exercise == null)
            {
                throw new ArgumentNullException(
                    string.Format(InvalidExerciseIdErrorMessage, exerciseId));
            }

            return exercise;
        }

        public async Task<PaginatedList<ExerciseViewModel>> GetExercisesByCategoryAsync(bool isAdmin, string userName, string exercisesCategory, int? pageIndex = null)
        {
            var result = this.exercisesRepository.All();

            if (exercisesCategory != "All")
            {
                if (exercisesCategory == "Custom")
                {
                    if (isAdmin)
                    {
                        // if User is in role "Admin", select all custom exercises:
                        result = result.Where(e => e.IsCustom == true);
                    }
                    else
                    {
                        // select only the custom exercises for the current User:
                        result = result.Where(e => e.IsCustom == true && e.CreatorName == userName);
                    }
                }
                else
                {
                    // select exercises by chosen category:
                    result = result.Where(e => e.MuscleGroup == (MuscleGroup)Enum.Parse(typeof(MuscleGroup), exercisesCategory) && e.IsCustom == false);
                }
            }
            else
            {
                // select all exercises wich are not custom created:
                result = result.Where(e => e.IsCustom == false);
            }

            return await PaginatedList<ExerciseViewModel>.CreateAsync(result.OrderByDescending(r => r.CreatedOn).To<ExerciseViewModel>(), pageIndex ?? GlobalConstants.PaginationDefaultPageIndex, GlobalConstants.PaginationPageSize);
        }

        public async Task<PaginatedList<ExerciseViewModel>> GetExercisesByKeyWordAsync(string keyWord, int? pageIndex = null)
        {
            var result = this.exercisesRepository.All()
                             .Where(e => e.Name.ToLower().Contains(keyWord.ToLower())
                             || e.Description.ToLower().Contains(keyWord.ToLower()));

            return await PaginatedList<ExerciseViewModel>.CreateAsync(result.OrderByDescending(r => r.CreatedOn).To<ExerciseViewModel>(), pageIndex ?? GlobalConstants.PaginationDefaultPageIndex, GlobalConstants.PaginationPageSize);
        }
    }
}

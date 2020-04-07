namespace MyFitScope.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyFitScope.Data.Common.Repositories;
    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Services.Mapping;
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

        public DetailsExerciseViewModel GetExerciseById(string exerciseId)
             => this.exercisesRepository.All()
                    .Where(e => e.Id == exerciseId)
                    .To<DetailsExerciseViewModel>()
                    .FirstOrDefault();

        public ICollection<ExerciseViewModel> GetExercisesByCategory(string userName, string exerciseCategoryInput = null)
        {
            var result = this.exercisesRepository.All();

            if (exerciseCategoryInput != null)
            {
                if (exerciseCategoryInput == "Custom")
                {
                    result = result.Where(e => e.CreatorName == userName);
                }
                else
                {
                    result = result.Where(e => e.MuscleGroup == (MuscleGroup)Enum.Parse(typeof(MuscleGroup), exerciseCategoryInput) && e.IsCustom == false);
                }
            }
            else
            {
                result = result.Where(e => e.IsCustom == false);
            }

            return result.To<ExerciseViewModel>().ToList();
        }
    }
}

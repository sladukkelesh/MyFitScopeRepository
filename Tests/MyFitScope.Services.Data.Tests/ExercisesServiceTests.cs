namespace MyFitScope.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Moq;
    using MyFitScope.Data.Common.Repositories;
    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Data.Repositories;
    using MyFitScope.Services.Data.Tests.Common;
    using MyFitScope.Web.ViewModels.Exercises;
    using Xunit;

    public class ExercisesServiceTests
    {
        [Fact]
        public async Task TestCreateExerciseAsync_WithValidData_ShouldCreateExerciseCorrectly()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var repository = new EfDeletableEntityRepository<Exercise>(context);
            var service = new ExercisesService(repository);

            await service.CreateExerciseAsync("mama", "111", MuscleGroup.Abs, "Some Description", "MyName", false);

            var expected = "mama";
            var targetExercise = repository.All().Where(e => e.Name == "mama").FirstOrDefault();
            Assert.Equal(expected, targetExercise.Name);
        }

        [Fact]
        public async Task TestDeleteExerciseAsync_WithValidData_ShouldDeleteExerciseCorrectly()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var repository = new EfDeletableEntityRepository<Exercise>(context);

            var exerciseId = "asdf";
            await repository.AddAsync(new Exercise { Id = exerciseId });
            await repository.SaveChangesAsync();

            var service = new ExercisesService(repository);

            await service.DeleteExerciseAsync(exerciseId);

            var deletedExercise = repository.AllWithDeleted().Where(e => e.Id == exerciseId).FirstOrDefault();

            Assert.True(deletedExercise.IsDeleted);
        }

        [Fact]
        public async Task TestEditExerciseAsync_WithValidData_ShouldEditExerciseCorrectly()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var repository = new EfDeletableEntityRepository<Exercise>(context);

            var exerciseId = "asdf";
            await repository.AddAsync(new Exercise { Id = exerciseId });
            await repository.SaveChangesAsync();

            var service = new ExercisesService(repository);

            await service.EditExerciseAsync(exerciseId, "mama", "111", MuscleGroup.Abs, "Some Exercise");

            var updatedExercise = repository.All().Where(e => e.Id == exerciseId).FirstOrDefault();

            Assert.Equal("mama", updatedExercise.Name);
            Assert.Equal("Some Exercise", updatedExercise.Description);
            Assert.Equal("111", updatedExercise.VideoUrl);
        }

        [Theory]
        [InlineData("Some Name")]
        [InlineData("Second Name")]
        public void TestExerciseNameAlreadyExists_WithValidData_ShouldReturnTrue(string name)
        {
            var repository = new Mock<IDeletableEntityRepository<Exercise>>();
            repository.Setup(r => r.All()).Returns(new List<Exercise>()
                                                        {
                                                            new Exercise { Name = "Some Name" },
                                                            new Exercise { Name = "Second Name" },
                                                        }.AsQueryable());

            var service = new ExercisesService(repository.Object);
            Assert.True(service.ExerciseNameAlreadyExists(name));
        }

        [Fact]
        public async Task TestGetExerciseById_WithValidData_ShouldReturnCorrectExercise()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var repository = new EfDeletableEntityRepository<Exercise>(context);

            var exerciseId = "asdf";
            await repository.AddAsync(new Exercise { Id = exerciseId });
            await repository.SaveChangesAsync();

            var service = new ExercisesService(repository);

            var exercise = service.GetExerciseById<ExerciseViewModel>(exerciseId);

            Assert.Equal(exerciseId, exercise.Id);
            Assert.IsType<ExerciseViewModel>(exercise);
        }

        [Fact]
        public async Task TestGetExercisesByCategoryAsync_WithValidData_ShouldReturnCorrectExercises()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var repository = new EfDeletableEntityRepository<Exercise>(context);

            await repository.AddAsync(new Exercise { Name = "first", MuscleGroup = MuscleGroup.Abs });
            await repository.AddAsync(new Exercise { Name = "second", MuscleGroup = MuscleGroup.Abs });
            await repository.AddAsync(new Exercise { Name = "third", MuscleGroup = MuscleGroup.Shoulders });
            await repository.AddAsync(new Exercise { Name = "fourth", MuscleGroup = MuscleGroup.Upper_Legs });

            await repository.SaveChangesAsync();

            var service = new ExercisesService(repository);

            var exercises = await service.GetExercisesByCategoryAsync(false, null, MuscleGroup.Abs.ToString(), null);

            Assert.Equal(2, exercises.Count);
            Assert.Equal("first", exercises[0].Name);
            Assert.Equal("second", exercises[1].Name);
        }

        [Theory]
        [InlineData("first")]
        [InlineData("fi")]
        public async Task TestGetExercisesByKeyWordAsync_WithValidData_ShouldReturnCorrectExercises(string keyWord)
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var repository = new EfDeletableEntityRepository<Exercise>(context);

            await repository.AddAsync(new Exercise { Name = "first", MuscleGroup = MuscleGroup.Abs });
            await repository.AddAsync(new Exercise { Name = "second", MuscleGroup = MuscleGroup.Abs });
            await repository.AddAsync(new Exercise { Name = "third", MuscleGroup = MuscleGroup.Shoulders });
            await repository.AddAsync(new Exercise { Name = "fourth", MuscleGroup = MuscleGroup.Upper_Legs });

            await repository.SaveChangesAsync();

            var service = new ExercisesService(repository);

            var exercises = await service.GetExercisesByKeyWordAsync(keyWord);

            Assert.Equal("first", exercises[0].Name);
            Assert.Single(exercises);
        }
    }
}

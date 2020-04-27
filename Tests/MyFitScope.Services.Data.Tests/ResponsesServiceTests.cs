namespace MyFitScope.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MyFitScope.Data.Models.BlogModels;
    using MyFitScope.Data.Repositories;
    using MyFitScope.Services.Data.Tests.Common;
    using Xunit;

    public class ResponsesServiceTests
    {
        [Fact]
        public async Task TestCreateExerciseAsync_WithValidData_ShouldCreateExerciseCorrectly()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var repository = new EfDeletableEntityRepository<Response>(context);
            var service = new ResponsesService(repository);

            await service.CreateResponseAsync("Some Content", "111", "222", "333");

            var expected = "Some Content";
            var targetResponse = repository.All().Where(c => c.Content == expected).FirstOrDefault();
            Assert.Equal(expected, targetResponse.Content);
        }

        [Fact]
        public async Task TestDeleteCommentAsync_WithValidData_ShouldCreateExerciseCorrectly()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var repository = new EfDeletableEntityRepository<Response>(context);

            var responseId = "asdf";
            await repository.AddAsync(new Response { Id = responseId });
            await repository.SaveChangesAsync();

            var service = new ResponsesService(repository);

            await service.DeleteResponseAsync(responseId);

            var deletedComment = repository.AllWithDeleted().Where(c => c.Id == responseId).FirstOrDefault();

            Assert.True(deletedComment.IsDeleted);
        }

        [Fact]
        public async Task TestDeleteCommentAsync_WithInvalidData_ShouldThrowError()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var repository = new EfDeletableEntityRepository<Response>(context);

            var responsetId = "asdf";
            await repository.AddAsync(new Response { Id = responsetId });
            await repository.SaveChangesAsync();

            var service = new ResponsesService(repository);

            var invalidId = "aaaa";
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await service.DeleteResponseAsync(invalidId);
            });
        }
    }
}

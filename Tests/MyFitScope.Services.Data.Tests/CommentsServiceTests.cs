namespace MyFitScope.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MyFitScope.Data.Models.BlogModels;
    using MyFitScope.Data.Repositories;
    using MyFitScope.Services.Data.Tests.Common;
    using Xunit;

    public class CommentsServiceTests
    {
        [Fact]
        public async Task TestCreateExerciseAsync_WithValidData_ShouldCreateExerciseCorrectly()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var repository = new EfDeletableEntityRepository<Comment>(context);
            var service = new CommentsService(repository);

            await service.CreateCommentAsync("Some Content", "asdf", "1234");

            var expected = "Some Content";
            var targetComment = repository.All().Where(c => c.Content == expected).FirstOrDefault();
            Assert.Equal(expected, targetComment.Content);
        }

        [Fact]
        public async Task TestDeleteCommentAsync_WithValidData_ShouldCreateExerciseCorrectly()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var repository = new EfDeletableEntityRepository<Comment>(context);

            var commentId = "asdf";
            await repository.AddAsync(new Comment { Id = commentId });
            await repository.SaveChangesAsync();

            var service = new CommentsService(repository);

            await service.DeleteCommentAsync(commentId);

            var deletedComment = repository.AllWithDeleted().Where(c => c.Id == commentId).FirstOrDefault();

            Assert.True(deletedComment.IsDeleted);
        }

        [Fact]
        public async Task TestDeleteCommentAsync_WithInvalidData_ShouldThrowError()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var repository = new EfDeletableEntityRepository<Comment>(context);

            var commentId = "asdf";
            await repository.AddAsync(new Comment { Id = commentId });
            await repository.SaveChangesAsync();

            var service = new CommentsService(repository);

            var invalidId = "aaaa";
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await service.DeleteCommentAsync(invalidId);
            });
        }
    }
}

namespace MyFitScope.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MyFitScope.Data.Common.Repositories;
    using MyFitScope.Data.Models.BlogModels;
    using MyFitScope.Services.Mapping;
    using MyFitScope.Web.ViewModels.Comments;

    public class CommentsService : ICommentsService
    {
        private const string InvalidCommentIdErrorMessage = "Comment with ID: {0} does not exist.";

        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public async Task CreateComment(string commentContent, string articleId, string userId)
        {
            var comment = new Comment
            {
                Content = commentContent,
                ArticleId = articleId,
                UserId = userId,
                CreatedOn = DateTime.UtcNow,
            };

            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(string commentId)
        {
            var commentToDelete = await this.commentsRepository.GetByIdWithDeletedAsync(commentId);

            if (commentToDelete == null)
            {
                throw new ArgumentNullException(
                    string.Format(InvalidCommentIdErrorMessage, commentId));
            }

            commentToDelete.IsDeleted = true;

            await this.commentsRepository.SaveChangesAsync();
        }
    }
}

namespace MyFitScope.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using MyFitScope.Data.Common.Repositories;
    using MyFitScope.Data.Models.BlogModels;

    public class CommentsService : ICommentsService
    {
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
    }
}

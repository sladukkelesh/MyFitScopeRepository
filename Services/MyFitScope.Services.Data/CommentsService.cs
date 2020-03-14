namespace MyFitScope.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text;
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

        public async Task Create(string commentContent, string articleId, string userId)
        {
            var comment = new Comment
            {
                UserId = userId,
                ArticleId = articleId,
                Content = commentContent,
            };

            await this.commentsRepository.AddAsync(comment);
        }
    }
}

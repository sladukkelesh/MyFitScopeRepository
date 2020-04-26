namespace MyFitScope.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using MyFitScope.Data.Common.Repositories;
    using MyFitScope.Data.Models.BlogModels;

    public class ResponsesService : IResponsesService
    {
        private readonly IDeletableEntityRepository<Response> responsesRepository;

        public ResponsesService(IDeletableEntityRepository<Response> responsesRepository)
        {
            this.responsesRepository = responsesRepository;
        }

        public async Task CreateResponseAsync(string responseContent, string parentCommentId, string articleId, string userId)
        {
            var response = new Response
            {
                Content = responseContent,
                CommentId = parentCommentId,
                UserId = userId,
                CreatedOn = DateTime.UtcNow,
                ArticleId = articleId,
            };

            await this.responsesRepository.AddAsync(response);
            await this.responsesRepository.SaveChangesAsync();
        }

        public async Task DeleteResponseAsync(string responseId)
        {
            var responseToDelete = await this.responsesRepository.GetByIdWithDeletedAsync(responseId);

            responseToDelete.IsDeleted = true;

            await this.responsesRepository.SaveChangesAsync();
        }
    }
}

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

        public async Task CreateResponseAsync(string responseContent, string parentCommentId, string userId)
        {
            var response = new Response
            {
                Content = responseContent,
                CommentId = parentCommentId,
                UserId = userId,
                CreatedOn = DateTime.UtcNow,
            };

            await this.responsesRepository.AddAsync(response);
            await this.responsesRepository.SaveChangesAsync();
        }
    }
}

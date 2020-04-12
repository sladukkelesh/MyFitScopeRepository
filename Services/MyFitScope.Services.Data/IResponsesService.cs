namespace MyFitScope.Services.Data
{
    using System.Threading.Tasks;

    public interface IResponsesService
    {
        Task CreateResponseAsync(string responseContent, string parentCommentId, string articleId, string userId);

        Task DeleteResponseAsync(string responseId);
    }
}

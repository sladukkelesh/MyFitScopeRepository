namespace MyFitScope.Services.Data
{
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task CreateCommentAsync(string commentContent, string articleId, string userId);

        Task DeleteCommentAsync(string commentId);
    }
}

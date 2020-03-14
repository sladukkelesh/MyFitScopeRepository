namespace MyFitScope.Services.Data
{
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task CreateComment(string commentContent, string articleId, string userId);
    }
}

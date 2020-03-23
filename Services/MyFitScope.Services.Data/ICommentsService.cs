namespace MyFitScope.Services.Data
{
    using System.Threading.Tasks;

    using MyFitScope.Web.ViewModels.Comments;

    public interface ICommentsService
    {
        Task CreateComment(string commentContent, string articleId, string userId);
    }
}

namespace MyFitScope.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyFitScope.Data.Models.BlogModels.Enums;
    using MyFitScope.Web.Infrastructure;
    using MyFitScope.Web.ViewModels.Articles;

    public interface IArticlesService
    {
        Task<string> CreateArticle(string articleTitle, ArticleCategory articleCategory, string articleImageUrl, string articleContent, string userId);

        Task<PaginatedList<ArticleViewModel>> GetArticlesByCategoryAsync(string articleCategory, int? pageIndex);

        Task<PaginatedList<ArticleViewModel>> GetArticlesByKeyWordAsync(string keyWord, int? pageIndex);

        T GetArticleById<T>(string articleId);

        Task DeleteArticleAsync(string articleId);

        Task UpdateArticleAsync(string articleId, string articleTitle, ArticleCategory articleCategory, string articleImageUrl, string articleContent);
    }
}

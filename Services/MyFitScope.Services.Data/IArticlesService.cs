namespace MyFitScope.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyFitScope.Data.Models.BlogModels.Contracts;
    using MyFitScope.Web.ViewModels.Articles;

    public interface IArticlesService
    {
        Task CreateArticle(string articleTitle, ArticleCategory articleCategory, string articleImageUrl, string articleContent, string userId);

        IEnumerable<ArticleViewModel> GetAllArticles();

        IEnumerable<ArticleViewModel> GetArticlesByCategory(string articleCategory);

        DetailsArticleViewModel GetArticleById(string articleId);

        Task DeleteArticleAsync(string articleId);
    }
}

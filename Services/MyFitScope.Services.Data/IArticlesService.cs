namespace MyFitScope.Services.Data
{
    using System.Collections.Generic;

    using MyFitScope.Web.ViewModels.Articles;

    public interface IArticlesService
    {
        IEnumerable<ArticleViewModel> GetAllArticles();

        IEnumerable<ArticleViewModel> GetArticlesByCategory(int articleCategory);

        DetailsArticleViewModel GetArticleById(string articleId);
    }
}

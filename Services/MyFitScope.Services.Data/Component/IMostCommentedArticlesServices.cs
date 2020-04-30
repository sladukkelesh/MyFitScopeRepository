namespace MyFitScope.Services.Data.Component
{
    using System.Collections.Generic;

    using MyFitScope.Web.ViewModels.MostCommentedArticles;

    public interface IMostCommentedArticlesServices
    {
        IEnumerable<MostCommentedArticleViewModel> GetMostCommentedArticles();
    }
}

namespace MyFitScope.Web.ViewModels.Articles
{
    using Ganss.XSS;
    using MyFitScope.Web.Infrastructure;

    public class ArticlesLIstingViewModel
    {
        public PaginatedList<ArticleViewModel> Articles { get; set; }

        public string ArticleCategory { get; set; }
    }
}

namespace MyFitScope.Web.ViewModels.Articles
{
    using MyFitScope.Web.Infrastructure;

    public class SearchArticlesViewModel
    {
        public PaginatedList<ArticleViewModel> Articles { get; set; }

        public string KeyWord { get; set; }

        public string PageTitle { get; set; }

        public string NoResultsMessage { get; set; }
    }
}

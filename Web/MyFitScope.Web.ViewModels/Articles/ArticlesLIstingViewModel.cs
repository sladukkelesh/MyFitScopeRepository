﻿namespace MyFitScope.Web.ViewModels.Articles
{
    using Ganss.XSS;
    using MyFitScope.Web.Infrastructure;

    public class ArticlesLIstingViewModel
    {
        public PaginatedList<ArticleViewModel> Articles { get; set; }

        public string ArticlesCategory { get; set; }

        public string PageTitle { get; set; }

        public string NoResultsMessage { get; set; }
    }
}

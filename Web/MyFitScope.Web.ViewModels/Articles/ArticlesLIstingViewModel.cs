namespace MyFitScope.Web.ViewModels.Articles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MyFitScope.Data.Models.BlogModels.Enums;
    using MyFitScope.Web.Infrastructure;

    public class ArticlesLIstingViewModel
    {
        public PaginatedList<ArticleViewModel> Articles { get; set; }

        public string ArticleCategory { get; set; }
    }
}

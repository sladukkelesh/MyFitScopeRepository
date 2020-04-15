namespace MyFitScope.Web.ViewModels.Articles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MyFitScope.Data.Models.BlogModels.Enums;

    public class ArticlesLIstingViewModel
    {
        public IEnumerable<ArticleViewModel> Articles { get; set; }

        public ICollection<string> ArticleCategoriesTitles
        {
            get
            {
                return Enum.GetNames(typeof(ArticleCategory))
                        .ToList();
            }
        }
    }
}

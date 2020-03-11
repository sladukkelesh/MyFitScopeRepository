namespace MyFitScope.Web.ViewModels.Articles
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllArticlesViewModel
    {
        public ICollection<ArticleViewModel> Articles { get; set; }
    }
}

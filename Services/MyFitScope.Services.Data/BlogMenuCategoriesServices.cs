namespace MyFitScope.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MyFitScope.Data.Models.BlogModels.Contracts;
    using MyFitScope.Web.ViewModels.BlogMenuArticlesCategories;

    public class BlogMenuCategoriesServices : IBlogMenuCategoriesServices
    {
        public IEnumerable<ArticleCategoryViewModel> GetArticleCategories()
                 => Enum.GetValues(typeof(ArticleCategory))
                        .Cast<ArticleCategory>()
                        .Select(ac => new ArticleCategoryViewModel
                        {
                            CategoryType = ac.ToString(),
                        })
                        .ToList();
    }
}

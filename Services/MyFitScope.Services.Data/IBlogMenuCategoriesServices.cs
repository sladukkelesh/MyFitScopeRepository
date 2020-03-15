namespace MyFitScope.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyFitScope.Web.ViewModels.BlogMenuArticlesCategories;

    public interface IBlogMenuCategoriesServices
    {
        IEnumerable<ArticleCategoryViewModel> GetArticleCategories();
    }
}

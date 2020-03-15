namespace MyFitScope.Web.ViewComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Services.Data;
    using MyFitScope.Web.ViewModels.BlogMenuArticlesCategories;

    [ViewComponent(Name ="ArticlesCategories")]
    public class BlogMenuCategoriesComponent : ViewComponent
    {
        private readonly IBlogMenuCategoriesServices blogMenuCategoriesServices;

        public BlogMenuCategoriesComponent(IBlogMenuCategoriesServices blogMenuCategoriesServices)
        {
            this.blogMenuCategoriesServices = blogMenuCategoriesServices;
        }

        public IViewComponentResult Invoke()
        {
            var model = new ArticlesCategoriesMenuViewModel
            {
                Categories = this.blogMenuCategoriesServices.GetArticleCategories(),
            };

            return this.View(model);
        }
    }
}

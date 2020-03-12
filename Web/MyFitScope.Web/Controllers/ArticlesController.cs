namespace MyFitScope.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Services.Data;
    using MyFitScope.Web.ViewModels.Articles;

    public class ArticlesController : BaseController
    {
        private readonly IArticlesService articlesService;

        public ArticlesController(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
        }

        public IActionResult All()
        {
            var model = new AllArticlesViewModel();

            model.Articles = this.articlesService.GetAllArticles();

            return this.View(model);
        }

        public IActionResult Details(string articleId)
        {
            var model = this.articlesService.GetArticleById(articleId);

            return this.View(model);
        }
    }
}

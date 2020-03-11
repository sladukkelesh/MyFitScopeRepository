namespace MyFitScope.Web.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Data.Common.Repositories;
    using MyFitScope.Data.Models.BlogModels;
    using MyFitScope.Services.Mapping;
    using MyFitScope.Web.ViewModels.Articles;

    public class ArticlesController : BaseController
    {
        private readonly IDeletableEntityRepository<Article> articlesRepository;

        public ArticlesController(IDeletableEntityRepository<Article> articlesRepository)
        {
            this.articlesRepository = articlesRepository;
        }

        public IActionResult All()
        {
            var model = new AllArticlesViewModel();

            model.Articles = this.articlesRepository.All()
                .To<ArticleViewModel>()
                .ToList();

            return this.View(model);
        }

        public IActionResult Details(string articleId)
        {
            var model = this.articlesRepository.All()
                .Where(a => a.Id == articleId)
                .To<DetailsArticleViewModel>()
                .FirstOrDefault();

            return this.View(model);
        }
    }
}

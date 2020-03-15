namespace MyFitScope.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Data.Models;
    using MyFitScope.Data.Models.BlogModels.Contracts;
    using MyFitScope.Services.Data;
    using MyFitScope.Web.ViewModels.Articles;

    public class ArticlesController : BaseController
    {
        private readonly IArticlesService articlesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ArticlesController(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
        }

        public IActionResult All()
        {
            var model = new AllArticlesViewModel
            {
                Articles = this.articlesService.GetAllArticles(),
            };

            return this.View(model);
        }

        public IActionResult ArticlesByCategory(string articleCategory)
        {
            var model = new ArticlesByCategoryViewModel
            {
                Articles = this.articlesService.GetArticlesByCategory(articleCategory),
            };

            return this.View(model);
        }

        public IActionResult Details(string articleId)
        {
            var model = this.articlesService.GetArticleById(articleId);

            return this.View(model);
        }

        public IActionResult CreateArticle()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle(string articleTitle, ArticleCategory articleCategory, string articleImageUrl, string articleContent)
        {
            // Find a way to get the real Id of the User who is logged in!!!!!!!!!!!!!!!!!1
            var userId = "949c08e0-2ac8-4b37-882c-65c9a38b64f2";
            await this.articlesService.CreateArticle(articleTitle, articleCategory, articleImageUrl, articleContent, userId);

            return this.Redirect("/");
        }
    }
}

namespace MyFitScope.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Data;
    using MyFitScope.Data.Models;
    using MyFitScope.Data.Models.BlogModels.Enums;
    using MyFitScope.Services.Data;
    using MyFitScope.Web.ViewModels.Articles;

    public class ArticlesController : BaseController
    {
        private readonly IArticlesService articlesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext db;

        public ArticlesController(IArticlesService articlesService, UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            this.articlesService = articlesService;
            this.userManager = userManager;
            this.db = db;
        }

        public async Task<IActionResult> ArticlesListing(string articleCategory, int? pageIndex = null)
        {
            var model = new ArticlesLIstingViewModel();

            if (Enum.GetNames(typeof(ArticleCategory)).Any(ac => ac == articleCategory) || articleCategory == "All")
            {
                model.Articles = await this.articlesService.GetArticlesByCategoryAsync(articleCategory, pageIndex);
            }
            else
            {
                model.Articles = await this.articlesService.GetArticlesByKeyWordAsync(articleCategory, pageIndex);
            }

            model.ArticleCategory = articleCategory;

            return this.View(model);
        }

        public IActionResult Details(string articleId)
        {
            var model = this.articlesService.GetArticleById<DetailsArticleViewModel>(articleId);

            return this.View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CreateArticle()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle(CreateArticleInputViewModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            var articleId = await this.articlesService.CreateArticle(input.ArticleTitle, input.ArticleCategory, input.ArticleImageUrl, input.ArticleContent, userId);

            return this.RedirectToAction(nameof(this.Details), new { articleId });
        }

        public async Task<IActionResult> DeleteArticle(string articleId)
        {
            await this.articlesService.DeleteArticleAsync(articleId);

            return this.RedirectToAction("ArticlesListing");
        }

        [Authorize(Roles="Admin")]
        public IActionResult EditArticle(string articleId)
        {
            var model = this.articlesService.GetArticleById<EditArticleInputViewModel>(articleId);

            return this.View(model);
        }

        [Authorize(Roles="Admin")]
        [HttpPost]
        public async Task<IActionResult> EditArticle(EditArticleInputViewModel input)
        {
             await this.articlesService.UpdateArticleAsync(input.Id, input.Title, input.ArticleCategory, input.ImageUrl, input.Content);

             return this.RedirectToAction(nameof(this.Details), new { articleId = input.Id });
        }
    }
}

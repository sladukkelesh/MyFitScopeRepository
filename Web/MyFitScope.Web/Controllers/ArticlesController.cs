namespace MyFitScope.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Data;
    using MyFitScope.Data.Models;
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

        public IActionResult ArticlesListing(string articleCategory)
        {
            var model = new ArticlesLIstingViewModel
            {
                Articles = this.articlesService.GetArticlesByCategory(articleCategory),
            };

            return this.View(model);
        }

        public IActionResult Details(string articleId)
        {
            var model = this.articlesService.GetArticleById<DetailsArticleViewModel>(articleId);

            return this.View(model);
        }

        public IActionResult CreateArticle()
        {
            var currentUserWorkout = this.User.Claims.FirstOrDefault(c => c.Type == "WorkoutId").Value;
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

        public IActionResult EditArticle(string articleId)
        {
            var model = this.articlesService.GetArticleById<EditArticleInputViewModel>(articleId);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditArticle(EditArticleInputViewModel input)
        {
             await this.articlesService.UpdateArticleAsync(input.Id, input.Title, input.ArticleCategory, input.ImageUrl, input.Content);

             return this.RedirectToAction(nameof(this.Details), new { articleId = input.Id });
        }
    }
}

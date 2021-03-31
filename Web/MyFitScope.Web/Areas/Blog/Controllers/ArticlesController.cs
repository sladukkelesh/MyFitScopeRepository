namespace MyFitScope.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Ganss.XSS;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using MyFitScope.Common;
    using MyFitScope.Data.Models;
    using MyFitScope.Data.Models.BlogModels.Enums;
    using MyFitScope.Services.Data;
    using MyFitScope.Web.ViewModels.Articles;

    [Area("Blog")]
    public class ArticlesController : BaseController
    {
        private readonly IArticlesService articlesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ArticlesController(IArticlesService articlesService, UserManager<ApplicationUser> userManager)
        {
            this.articlesService = articlesService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> ArticlesListing(string articleCategory, int? pageIndex = null)
        {
            if (articleCategory == null)
            {
                articleCategory = "All";
            }
            else
            {
                articleCategory = new HtmlSanitizer().Sanitize(articleCategory);
            }

            var model = new ArticlesLIstingViewModel();

            if (Enum.GetNames(typeof(ArticleCategory)).Any(ac => ac == articleCategory) || articleCategory == "All")
            {
                model.Articles = await this.articlesService.GetArticlesByCategoryAsync(articleCategory, pageIndex);
                model.ArticleCategory = "listing=" + articleCategory;
            }
            else
            {
                model.Articles = await this.articlesService.GetArticlesByKeyWordAsync(articleCategory, pageIndex);
                model.ArticleCategory = "search=" + articleCategory;
            }

            return this.View(model);
        }

        public IActionResult Details(string articleId)
        {
            var model = this.articlesService.GetArticleById<DetailsArticleViewModel>(articleId);

            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult CreateArticle()
        {
            return this.View();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> CreateArticle(CreateArticleInputViewModel input)
        {
            var titleAlreadyExists = this.articlesService.ArticleTitleAlreadyExists(input.ArticleTitle);

            if (!this.ModelState.IsValid || titleAlreadyExists)
            {
                if (titleAlreadyExists)
                {
                    this.TempData["error"] = string.Format("Article with title \"{0}\" already exists! Please, choose another title!", input.ArticleTitle);
                }

                return this.View(input);
            }

            var userId = this.userManager.GetUserId(this.User);
            var articleId = await this.articlesService.CreateArticle(input.ArticleTitle, input.ArticleCategory, input.ArticleContent, userId, input.ArticleImage);

            return this.RedirectToAction(nameof(this.Details), new { articleId });
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> DeleteArticle(string articleId)
        {
            await this.articlesService.DeleteArticleAsync(articleId);

            return this.RedirectToAction("ArticlesListing", new { articleCategory = "All" });
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult EditArticle(string articleId)
        {
            var model = this.articlesService.GetArticleById<EditArticleInputViewModel>(articleId);

            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> EditArticle(EditArticleInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.articlesService.UpdateArticleAsync(input.Id, input.Title, input.ArticleCategory, input.ArticleImage, input.Content);

            return this.RedirectToAction(nameof(this.Details), new { articleId = input.Id });
        }
    }
}

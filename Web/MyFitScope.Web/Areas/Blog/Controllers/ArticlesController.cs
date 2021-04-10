namespace MyFitScope.Web.Controllers
{
    using System.Threading.Tasks;

    using Ganss.XSS;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using MyFitScope.Common;
    using MyFitScope.Data.Models;
    using MyFitScope.Services.Data;
    using MyFitScope.Web.ViewModels.Articles;

    [Area("Blog")]
    public class ArticlesController : BaseController
    {
        private const string ListingPageTitleAll = "All Articles";
        private const string ListingPageTitleCategory = "Articles of category \"{0}\"";
        private const string ListingPageNoResultsMessage = "Sorry, we don't have any articles in this category at this moment...";

        private readonly IArticlesService articlesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ArticlesController(IArticlesService articlesService, UserManager<ApplicationUser> userManager)
        {
            this.articlesService = articlesService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> ArticlesListing(string articlesCategory, int? pageIndex = null)
        {
            var model = new ArticlesLIstingViewModel
            {
                Articles = await this.articlesService.GetArticlesByCategoryAsync(articlesCategory, pageIndex),
                ArticlesCategory = articlesCategory,
                PageTitle = articlesCategory == "All" ? ListingPageTitleAll : string.Format(ListingPageTitleCategory, articlesCategory.Replace("_", " ")),
                NoResultsMessage = ListingPageNoResultsMessage,
            };

            return this.View(model);
        }

        public async Task<IActionResult> Search(string keyWord, int? pageIndex = null)
        {
            keyWord = new HtmlSanitizer().Sanitize(keyWord);

            var model = new SearchArticlesViewModel
            {
                Articles = await this.articlesService.GetArticlesByKeyWordAsync(keyWord, pageIndex),
                KeyWord = keyWord,
                PageTitle = string.Format(GlobalConstants.SearchPageTitle, keyWord),
                NoResultsMessage = string.Format(GlobalConstants.SearchPageNoResultsMessage, keyWord),
            };

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

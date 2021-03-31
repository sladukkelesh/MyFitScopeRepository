namespace MyFitScope.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using MyFitScope.Common;
    using MyFitScope.Data.Models;
    using MyFitScope.Services.Data;
    using MyFitScope.Web.ViewModels.Responses;

    [Area("Blog")]
    public class ResponsesController : BaseController
    {
        private const string MissingResponseContentExeption = "Response field is required!";
        private const string InvalidResponseContentSize = "Response size must be between 3 and 500 symbols!";

        private readonly IResponsesService responsesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ResponsesController(IResponsesService responsesService, UserManager<ApplicationUser> userManager)
        {
            this.responsesService = responsesService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddResponse(CreateResponseInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(input.ResponseContent))
                {
                    this.TempData["error"] = MissingResponseContentExeption;
                }
                else if (input.ResponseContent.Length < 3 || input.ResponseContent.Length > 500)
                {
                    this.TempData["error"] = InvalidResponseContentSize;
                }

                return this.RedirectToAction("Details", "Articles", new { articleId = input.ArticleId });
            }

            var userId = this.userManager.GetUserId(this.User);

            await this.responsesService.CreateResponseAsync(input.ResponseContent, input.ParentCommentId, input.ArticleId, userId);

            return this.RedirectToAction("Details", "Articles", new { articleId = input.ArticleId });
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> DeleteResponse(string responseId, string articleId)
        {
            await this.responsesService.DeleteResponseAsync(responseId);

            return this.RedirectToAction("Details", "Articles", new { articleId });
        }
    }
}

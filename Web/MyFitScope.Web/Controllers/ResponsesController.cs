namespace MyFitScope.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Data.Models;
    using MyFitScope.Services.Data;
    using MyFitScope.Web.ViewModels.Responses;

    public class ResponsesController : BaseController
    {
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
                return this.RedirectToAction("Details", "Articles", new { articleId = input.ArticleId });
            }

            var userId = this.userManager.GetUserId(this.User);

            await this.responsesService.CreateResponseAsync(input.ResponseContent, input.ParentCommentId, input.ArticleId, userId);

            return this.RedirectToAction("Details", "Articles", new { articleId = input.ArticleId });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteResponse(string responseId, string articleId)
        {
            await this.responsesService.DeleteResponseAsync(responseId);

            return this.RedirectToAction("Details", "Articles", new { articleId });
        }
    }
}

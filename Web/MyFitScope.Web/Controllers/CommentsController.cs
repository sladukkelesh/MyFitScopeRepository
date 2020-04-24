namespace MyFitScope.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Data.Models;
    using MyFitScope.Services.Data;
    using MyFitScope.Web.ViewModels.Comments;

    public class CommentsController : BaseController
    {
        private const string MissingCommentContentExeption = "Comment field is required!";
        private const string InvalidCommentContentSize = "Comment size must be between 3 and 500 symbols!";

        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(ICommentsService commentsService, UserManager<ApplicationUser> userManager)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment(CreateCommentInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(input.CommentContent))
                {
                    this.TempData["error"] = MissingCommentContentExeption;
                }
                else if (input.CommentContent.Length < 3 || input.CommentContent.Length > 500)
                {
                    this.TempData["error"] = InvalidCommentContentSize;
                }

                return this.RedirectToAction("Details", "Articles", new { articleId = input.ArticleId });
            }

            string userId = this.userManager.GetUserId(this.User);

            await this.commentsService.CreateComment(input.CommentContent, input.ArticleId, userId);

            return this.RedirectToAction("Details", "Articles", new { articleId = input.ArticleId });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteComment(string commentId, string articleId)
        {
            await this.commentsService.DeleteCommentAsync(commentId);

            return this.RedirectToAction("Details", "Articles", new { articleId });
        }
    }
}

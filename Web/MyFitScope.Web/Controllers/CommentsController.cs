namespace MyFitScope.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Data.Models;
    using MyFitScope.Services.Data;

    public class CommentsController : BaseController
    {
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(ICommentsService commentsService, UserManager<ApplicationUser> userManager)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(string commentContent, string articleId)
        {
            string userId = this.userManager.GetUserId(this.User);

            await this.commentsService.CreateComment(commentContent, articleId, userId);

            return this.Redirect("/");
        }
    }
}

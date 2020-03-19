namespace MyFitScope.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Services.Data;

    public class ResponsesController : BaseController
    {
        private readonly IResponsesService responsesService;

        public ResponsesController(IResponsesService responsesService)
        {
            this.responsesService = responsesService;
        }

        [HttpPost]
        public async Task<IActionResult> AddResponse(string responseContent, string parentCommentId)
        {
            var userId = "949c08e0-2ac8-4b37-882c-65c9a38b64f2";

            await this.responsesService.CreateResponseAsync(responseContent, parentCommentId, userId);

            return this.Redirect("/");
        }
    }
}

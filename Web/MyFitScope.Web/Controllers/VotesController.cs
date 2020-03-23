namespace MyFitScope.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Data.Models;
    using MyFitScope.Services.Data;
    using MyFitScope.Web.ViewModels.Votes;

    [ApiController]
    [Route("/api/[controller]")]
    public class VotesController : BaseController
    {
        private readonly IVotesService votesService;
        private readonly UserManager<ApplicationUser> userManager;

        public VotesController(IVotesService votesService, UserManager<ApplicationUser> userManager)
        {
            this.votesService = votesService;
            this.userManager = userManager;
        }

        // POST /api/votes
        // body
        // {"commentId" : "175377a9-5ceb-4542-aac3-8d2bed88b709", "isUpVote": true}
        // response {"UpVotes" : 1, "DownVotes": 0}
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<VoteOutputModel>> Post(VoteInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);

            await this.votesService.CreateCommentVoteAsync(input.CommentId, input.IsUpVote, userId);

            var result = this.votesService.GetCommentVotesCount(input.CommentId);

            return result;
        }
    }
}

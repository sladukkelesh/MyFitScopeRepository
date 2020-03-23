namespace MyFitScope.Services.Data
{
    using System.Threading.Tasks;

    using MyFitScope.Web.ViewModels.Votes;

    public interface IVotesService
    {
        Task CreateCommentVoteAsync(string commentId, bool isUpvote, string userId);

        VoteOutputModel GetCommentVotesCount(string commentId);
    }
}

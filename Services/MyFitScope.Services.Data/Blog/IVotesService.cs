namespace MyFitScope.Services.Data
{
    using System.Threading.Tasks;

    using MyFitScope.Web.ViewModels.Votes;

    public interface IVotesService
    {
        Task CreateVoteAsync(string votedObjectName, string commentId, bool isUpvote, string userId);

        VoteOutputModel GetVotesCount(string votedObjectName, string commentId);
    }
}

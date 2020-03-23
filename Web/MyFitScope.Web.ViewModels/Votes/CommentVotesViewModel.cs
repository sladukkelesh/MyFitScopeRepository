namespace MyFitScope.Web.ViewModels.Votes
{
    using MyFitScope.Data.Models;
    using MyFitScope.Services.Mapping;

    public class CommentVotesViewModel : IMapFrom<Vote>
    {
        public string CommentId { get; set; }

        public VoteType VoteType { get; set; }
    }
}

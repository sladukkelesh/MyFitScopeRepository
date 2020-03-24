namespace MyFitScope.Web.ViewModels.Votes
{
    using MyFitScope.Data.Models;
    using MyFitScope.Services.Mapping;

    public class ResponseVotesViewModel : IMapFrom<Vote>
    {
        public string ResponseId { get; set; }

        public VoteType VoteType { get; set; }
    }
}

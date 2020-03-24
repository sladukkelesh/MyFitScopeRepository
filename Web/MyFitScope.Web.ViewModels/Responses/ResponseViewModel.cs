namespace MyFitScope.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MyFitScope.Data.Models;
    using MyFitScope.Data.Models.BlogModels;
    using MyFitScope.Services.Mapping;
    using MyFitScope.Web.ViewModels.Votes;

    public class ResponseViewModel : IMapFrom<Response>
    {
        public string Id { get; set; }

        public string UserUserName { get; set; }

        public string Content { get; set; }

        public int PositiveVotes { get; set; }

        public int NegativeVotes { get; set; }

        public DateTime CreatedOn { get; set; }

        public string OutputDate
            => this.CreatedOn.Year + "-" + this.CreatedOn.Month + "-" + this.CreatedOn.Day;

        public IEnumerable<ResponseVotesViewModel> Votes { get; set; }

        public int UpVotes
                    => this.Votes.Count(v => v.VoteType == VoteType.UpVote);

        public int DownVotes
                    => this.Votes.Count(v => v.VoteType == VoteType.DownVote);
    }
}

namespace MyFitScope.Web.ViewModels.Votes
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class VoteInputModel
    {
        public string VotedObject { get; set; }

        public string VotedObjectId { get; set; }

        public bool IsUpVote { get; set; }
    }
}

namespace MyFitScope.Web.ViewModels.Votes
{
    using System.ComponentModel.DataAnnotations;

    public class VoteInputModel
    {
        [Required]
        public string VotedObject { get; set; }

        [Required]
        public string VotedObjectId { get; set; }

        [Required]
        public bool IsUpVote { get; set; }
    }
}

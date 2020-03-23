namespace MyFitScope.Data.Models
{
    using MyFitScope.Data.Common.Models;
    using MyFitScope.Data.Models.BlogModels;

    public class Vote : BaseModel<string>
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string CommentId { get; set; }

        public virtual Comment Comment { get; set; }

        public string ResponseId { get; set; }

        public virtual Response Response { get; set; }

        public VoteType VoteType { get; set; }
    }
}

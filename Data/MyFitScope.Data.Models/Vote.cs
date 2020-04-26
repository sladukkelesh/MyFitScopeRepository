namespace MyFitScope.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MyFitScope.Data.Common.Models;
    using MyFitScope.Data.Models.BlogModels;

    public class Vote : BaseModel<string>
    {
        public Vote()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string CommentId { get; set; }

        public virtual Comment Comment { get; set; }

        public string ResponseId { get; set; }

        public virtual Response Response { get; set; }

        [Required]
        public VoteType VoteType { get; set; }
    }
}

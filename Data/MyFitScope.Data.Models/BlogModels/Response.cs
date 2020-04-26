namespace MyFitScope.Data.Models.BlogModels
{
    using System;
    using System.Collections.Generic;

    using MyFitScope.Data.Common.Models;

    public class Response : BaseDeletableModel<string>
    {
        public Response()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
            this.Votes = new HashSet<Vote>();
        }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string Content { get; set; }

        public int PositiveVotes { get; set; }

        public int NegativeVotes { get; set; }

        public string ArticleId { get; set; }

        public virtual Article Article { get; set; }

        public string CommentId { get; set; }

        public Comment Comment { get; set; }

        public ICollection<Vote> Votes { get; set; }
    }
}

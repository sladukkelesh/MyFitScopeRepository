namespace MyFitScope.Data.Models.BlogModels
{
    using System;
    using System.Collections.Generic;

    using MyFitScope.Data.Common.Models;

    public class Comment : BaseDeletableModel<string>
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Responses = new HashSet<Response>();
            this.PositiveVotes = 0;
            this.NegativeVotes = 0;
        }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string ArticleId { get; set; }

        public virtual Article Article { get; set; }

        public string Content { get; set; }

        public int PositiveVotes { get; set; }

        public int NegativeVotes { get; set; }

        public ICollection<Response> Responses { get; set; }
    }
}

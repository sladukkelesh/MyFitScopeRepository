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
        }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string Content { get; set; }

        public int PositiveVotes { get; set; }

        public int NegativeVotes { get; set; }

        public ICollection<Response> Responses { get; set; }
    }
}

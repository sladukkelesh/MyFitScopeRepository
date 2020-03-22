namespace MyFitScope.Data.Models.BlogModels
{
    using System;
    using System.Collections.Generic;

    using MyFitScope.Data.Common.Models;
    using MyFitScope.Data.Models.BlogModels.Enums;

    public class Article : BaseDeletableModel<string>
    {
        public Article()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Comments = new HashSet<Comment>();
        }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public ArticleCategory ArticleCategory { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}

namespace MyFitScope.Data.Models.BlogModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyFitScope.Data.Common.Models;
    using MyFitScope.Data.Models.BlogModels.Enums;

    public class Article : BaseDeletableModel<string>
    {
        public Article()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
            this.Comments = new HashSet<Comment>();
        }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public ArticleCategory ArticleCategory { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [MaxLength(20000)]
        public string Content { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string ImagePublicId { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}

﻿namespace MyFitScope.Web.ViewModels.Articles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Ganss.XSS;
    using MyFitScope.Data.Models.BlogModels;
    using MyFitScope.Services.Mapping;
    using MyFitScope.Web.ViewModels.Comments;

    public class DetailsArticleViewModel : IMapFrom<Article>
    {
        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserId { get; set; }

        public string UserUserName { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string SanitizedContent
            => new HtmlSanitizer().Sanitize(this.Content);

        public string ImageUrl { get; set; }

        public string CommentUrl
            => "/Comments/AddComment";

        public int TotalCommentsCount
        {
            get
            {
                return this.Comments.Count() + this.Comments.SelectMany(c => c.Responses).Count();
            }
        }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}

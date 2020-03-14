namespace MyFitScope.Web.ViewModels.Articles
{
    using System.Collections.Generic;

    using MyFitScope.Data.Models.BlogModels;
    using MyFitScope.Services.Mapping;
    using MyFitScope.Web.ViewModels.Comments;

    public class DetailsArticleViewModel : IMapFrom<Article>
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string UserUserName { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}

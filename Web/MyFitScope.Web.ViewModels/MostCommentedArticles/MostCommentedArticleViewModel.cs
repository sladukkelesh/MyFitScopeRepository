namespace MyFitScope.Web.ViewModels.MostCommentedArticles
{
    using System.Collections.Generic;
    using System.Linq;
    using MyFitScope.Data.Models.BlogModels;
    using MyFitScope.Services.Mapping;
    using MyFitScope.Web.ViewModels.Comments;

    public class MostCommentedArticleViewModel : IMapFrom<Article>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public int TotalCommentsCount
        {
            get
            {
                return this.Comments.Count() + this.Comments.SelectMany(c => c.Responses).Count();
            }
        }

        public ICollection<CommentViewModel> Comments { get; set; }
    }
}

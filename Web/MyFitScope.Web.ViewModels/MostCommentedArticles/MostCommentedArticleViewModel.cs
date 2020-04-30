namespace MyFitScope.Web.ViewModels.MostCommentedArticles
{
    using MyFitScope.Data.Models.BlogModels;
    using MyFitScope.Services.Mapping;

    public class MostCommentedArticleViewModel : IMapFrom<Article>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public int CommentsCount { get; set; }
    }
}

namespace MyFitScope.Web.ViewModels.Articles
{
    using MyFitScope.Data.Models.BlogModels;
    using MyFitScope.Services.Mapping;

    public class ArticleViewModel : IMapFrom<Article>
    {
        public string Id { get; set; }

        public string UserUserName { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }
    }
}

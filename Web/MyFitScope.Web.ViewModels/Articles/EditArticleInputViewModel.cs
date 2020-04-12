namespace MyFitScope.Web.ViewModels.Articles
{
    using MyFitScope.Data.Models.BlogModels;
    using MyFitScope.Data.Models.BlogModels.Enums;
    using MyFitScope.Services.Mapping;

    public class EditArticleInputViewModel : IMapFrom<Article>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public ArticleCategory ArticleCategory { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }
    }
}

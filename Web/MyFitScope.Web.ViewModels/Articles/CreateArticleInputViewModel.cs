using MyFitScope.Data.Models.BlogModels.Enums;

namespace MyFitScope.Web.ViewModels.Articles
{

    public class CreateArticleInputViewModel
    {
        public string ArticleTitle { get; set; }

        public ArticleCategory ArticleCategory { get; set; }

        public string ArticleImageUrl { get; set; }

        public string ArticleContent { get; set; }
    }
}

namespace MyFitScope.Web.ViewModels.Articles
{
    using System.ComponentModel.DataAnnotations;

    using MyFitScope.Data.Models.BlogModels.Enums;

    public class CreateArticleInputViewModel
    {
        [Required]
        [MaxLength(200)]
        [MinLength(3)]
        public string ArticleTitle { get; set; }

        [Required]
        public ArticleCategory ArticleCategory { get; set; }

        [Required]
        public string ArticleImageUrl { get; set; }

        [Required]
        [MaxLength(20000)]
        [MinLength(3)]
        public string ArticleContent { get; set; }
    }
}

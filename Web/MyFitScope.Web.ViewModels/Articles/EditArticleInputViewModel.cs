namespace MyFitScope.Web.ViewModels.Articles
{
    using System.ComponentModel.DataAnnotations;

    using MyFitScope.Data.Models.BlogModels;
    using MyFitScope.Data.Models.BlogModels.Enums;
    using MyFitScope.Services.Mapping;

    public class EditArticleInputViewModel : IMapFrom<Article>
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(200)]
        [MinLength(3)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Category")]
        public ArticleCategory ArticleCategory { get; set; }

        [Required]
        [MaxLength(20000)]
        [MinLength(3)]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}

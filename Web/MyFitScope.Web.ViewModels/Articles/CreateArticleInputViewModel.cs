namespace MyFitScope.Web.ViewModels.Articles
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using MyFitScope.Data.Models.BlogModels.Enums;

    public class CreateArticleInputViewModel
    {
        [Required]
        [MaxLength(200, ErrorMessage = "Article Title cannot be greater than 200 symbols!")]
        [MinLength(3, ErrorMessage = "Article Title cannot be smaller than 3 symbols!")]
        [Display(Name = "Title")]
        public string ArticleTitle { get; set; }

        [Required]
        [Display(Name = "Category")]
        public ArticleCategory ArticleCategory { get; set; }

        [Required]
        [MaxLength(20000, ErrorMessage = "Article Content cannot be greater than 20000 symbols!")]
        [MinLength(3, ErrorMessage = "Article Title cannot be smaller than 3 symbols!")]
        [Display(Name = "Content")]
        public string ArticleContent { get; set; }

        [Required]
        [Display(Name = "Upload Cover Image")]
        public IFormFile ArticleImage { get; set; }
    }
}

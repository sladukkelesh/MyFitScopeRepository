namespace MyFitScope.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class CreateCommentInputModel
    {
        [Required]
        public string ArticleId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(500)]
        public string CommentContent { get; set; }
    }
}

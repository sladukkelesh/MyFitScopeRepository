namespace MyFitScope.Web.ViewModels.Responses
{
    using System.ComponentModel.DataAnnotations;

    public class CreateResponseInputModel
    {
        [Required]
        public string ArticleId { get; set; }

        [Required]
        public string ParentCommentId { get; set; }

        [Required]
        [MaxLength(500)]
        [MinLength(3)]
        public string ResponseContent { get; set; }
    }
}

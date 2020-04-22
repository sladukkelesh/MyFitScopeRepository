namespace MyFitScope.Web.ViewModels.ProgressImages
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateProgressImageInputModel
    {
        [Required]
        public IFormFile ProgressImage { get; set; }
    }
}

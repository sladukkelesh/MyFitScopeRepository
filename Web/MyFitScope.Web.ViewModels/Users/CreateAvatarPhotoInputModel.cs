namespace MyFitScope.Web.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateAvatarPhotoInputModel
    {
        [Required]
        public IFormFile Photo { get; set; }
    }
}

namespace MyFitScope.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using MyFitScope.Web.ViewModels.ProgressImages;

    public interface IProgressImagesService
    {
        Task UploadProgressImageAsync(string userId, string userName, IFormFile file);

        Task DeleteProgressImage(string imageId);

        ICollection<ProgressImageViewModel> GetAllImages(string userId);
    }
}

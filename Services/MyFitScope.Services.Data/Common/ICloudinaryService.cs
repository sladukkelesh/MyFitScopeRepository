namespace MyFitScope.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using MyFitScope.Web.ViewModels.Cloudinary;

    public interface ICloudinaryService
    {
        Task<CloudinaryResultModel> UploadPhotoAsync(IFormFile file, string fileName, string folder);

        void DeletePhoto(string publicId);
    }
}

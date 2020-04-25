namespace MyFitScope.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using MyFitScope.Web.Infrastructure;
    using MyFitScope.Web.ViewModels.ProgressImages;

    public interface IProgressImagesService
    {
        Task UploadProgressImageAsync(string userId, string userName, IFormFile file);

        Task DeleteProgressImageAsync(string imageId);

        Task<PaginatedList<ProgressImageViewModel>> GetAllImagesAsync(string userId, int? pageIndex);
    }
}

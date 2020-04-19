namespace MyFitScope.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using MyFitScope.Common;
    using MyFitScope.Data.Common.Repositories;
    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Services.Mapping;
    using MyFitScope.Web.ViewModels.ProgressImages;
    using shortid;

    public class ProgressImagesService : IProgressImagesService
    {
        private readonly IDeletableEntityRepository<ProgressImage> progressImagesRepository;
        private readonly ICloudinaryService cloudinaryService;

        public ProgressImagesService(IDeletableEntityRepository<ProgressImage> progressImagesRepository, ICloudinaryService cloudinaryService)
        {
            this.progressImagesRepository = progressImagesRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task UploadProgressImageAsync(string userId, string userName, IFormFile file)
        {
            var fileName = userName + "_" + ShortId.Generate();
            var uploadPhotoResponse = await this.cloudinaryService.UploadPhotoAsync(file, fileName, GlobalConstants.CloudProgressImageFolder);

            var progressImage = new ProgressImage
            {
                UserId = userId,
                PublidId = uploadPhotoResponse.PublicId,
                Url = uploadPhotoResponse.PhotoUrl,
            };

            await this.progressImagesRepository.AddAsync(progressImage);
            await this.progressImagesRepository.SaveChangesAsync();
        }

        public async Task DeleteProgressImage(string imageId)
        {
            var imageToDelete = await this.progressImagesRepository.GetByIdWithDeletedAsync(imageId);

            imageToDelete.IsDeleted = true;

            await this.progressImagesRepository.SaveChangesAsync();
        }

        public ICollection<ProgressImageViewModel> GetAllImages(string userId)
        {
            var result = this.progressImagesRepository.All()
                .Where(pi => pi.UserId == userId)
                .To<ProgressImageViewModel>()
                .ToList();

            return result;
        }
    }
}

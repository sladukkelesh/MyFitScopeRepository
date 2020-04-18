namespace MyFitScope.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using MyFitScope.Common;
    using MyFitScope.Data.Common.Repositories;
    using MyFitScope.Data.Models;

    public class UsersService : IUsersService
    {
        private readonly ICloudinaryService cloudinaryService;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public UsersService(ICloudinaryService cloudinaryService, IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.cloudinaryService = cloudinaryService;
            this.usersRepository = usersRepository;
        }

        public async Task UpdateAvatarPhotoAsync(string loggedInUserId, IFormFile file)
        {
            var user = await this.usersRepository.GetByIdWithDeletedAsync(loggedInUserId);

            if (user.AvatarImageUrl != null)
            {
                this.cloudinaryService.DeletePhoto(user.AvatarImagePublicId);
            }

            var uploadPhotoResponse = await this.cloudinaryService.UploadPhotoAsync(file, user.UserName, GlobalConstants.CloudUsersImageFolder);

            user.AvatarImagePublicId = uploadPhotoResponse.PublicId;
            user.AvatarImageUrl = uploadPhotoResponse.PhotoUrl;

            await this.usersRepository.SaveChangesAsync();
        }
    }
}

namespace MyFitScope.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using MyFitScope.Common;
    using MyFitScope.Data.Common.Repositories;
    using MyFitScope.Data.Models;

    public class UsersService : IUsersService
    {
        private const string InvalidUserIdErrorMessage = "User with ID: {0} does not exist.";
        private const string InvalidCloudinaryResponseParams = "Missing response from Cloudinary!";

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

            if (user == null)
            {
                throw new ArgumentNullException(
                    string.Format(InvalidUserIdErrorMessage, loggedInUserId));
            }

            if (user.AvatarImageUrl != null)
            {
                this.cloudinaryService.DeletePhoto(user.AvatarImagePublicId);
            }

            var uploadPhotoResponse = await this.cloudinaryService.UploadPhotoAsync(file, user.UserName, GlobalConstants.CloudUsersImageFolder);

            if (uploadPhotoResponse == null)
            {
                throw new ArgumentNullException(InvalidCloudinaryResponseParams);
            }

            user.AvatarImagePublicId = uploadPhotoResponse.PublicId;
            user.AvatarImageUrl = uploadPhotoResponse.PhotoUrl;

            await this.usersRepository.SaveChangesAsync();
        }
    }
}

namespace MyFitScope.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using MyFitScope.Data.Models;

    public interface IUsersService
    {
        Task UpdateAvatarPhotoAsync(string loggedInUserId, IFormFile file);
    }
}

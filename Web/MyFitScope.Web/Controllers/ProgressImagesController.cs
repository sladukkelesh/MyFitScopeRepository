namespace MyFitScope.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Data.Models;
    using MyFitScope.Services.Data;
    using MyFitScope.Web.ViewModels.ProgressImages;

    public class ProgressImagesController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IProgressImagesService progressImagesService;

        public ProgressImagesController(UserManager<ApplicationUser> userManager, IProgressImagesService progressImagesService)
        {
            this.userManager = userManager;
            this.progressImagesService = progressImagesService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddProgressImage(CreateProgressImageInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.ProgressImagesListing));
            }

            var userId = this.userManager.GetUserId(this.User);
            var userName = this.User.Identity.Name;

            await this.progressImagesService.UploadProgressImageAsync(userId, userName, input.ProgressImage);

            return this.RedirectToAction("ProgressImagesListing", "ProgressImages");
        }

        [Authorize]
        public IActionResult ProgressImagesListing()
        {
            var userId = this.userManager.GetUserId(this.User);

            var model = new ProgressImagesListingViewModel
            {
                Images = this.progressImagesService.GetAllImages(userId).ToList(),
            };

            return this.View(model);
        }

        [Authorize]
        public async Task<IActionResult> DeleteProgressImage(string imageId)
        {
            await this.progressImagesService.DeleteProgressImageAsync(imageId);

            return this.RedirectToAction(nameof(this.ProgressImagesListing));
        }
    }
}

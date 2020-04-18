namespace MyFitScope.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Data.Models;
    using MyFitScope.Services.Data;
    using MyFitScope.Web.ViewModels.Users;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(IUsersService usersService, UserManager<ApplicationUser> userManager)
        {
            this.usersService = usersService;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddAvatarPhoto(IFormFile photo)
        {
            var loggedInUserId = this.userManager.GetUserId(this.User);

            await this.usersService.UpdateAvatarPhotoAsync(loggedInUserId, photo);

            return this.Redirect("/");
        }
    }
}

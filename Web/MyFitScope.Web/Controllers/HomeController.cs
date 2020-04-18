namespace MyFitScope.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Data.Models;
    using MyFitScope.Web.ViewModels;
    using MyFitScope.Web.ViewModels.Users;

    public class HomeController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var loggedInUser = await this.userManager.FindByIdAsync(this.userManager.GetUserId(this.User));

                var model = new UserLoggedInIndexPageViewModel
                {
                    AvatarImageUrl = loggedInUser.AvatarImageUrl,
                };

                return this.View("LoggedInIndexPage", model);
            }

            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}

namespace MyFitScope.Web.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using MyFitScope.Web.ViewModels;

    using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

    public class ErrorController : BaseController
    {
        [Route("/Error/500")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult InternalServerError()
        {
            var errorViewModel = new ErrorViewModel
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier,
            };

            return this.View(errorViewModel);
        }

        [Route("/Error/404")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult NotFoundError()
        {
            var errorViewModel = new ErrorViewModel();
            errorViewModel.StatusCode = StatusCodes.Status404NotFound;

            if (this.TempData["ErrorParams"] is Dictionary<string, string> errorDictionary)
            {
                errorViewModel.RequestId = errorDictionary["RequestId"];
                errorViewModel.RequestPath = errorDictionary["RequestPath"];
            }

            if (errorViewModel.RequestId == null)
            {
                errorViewModel.RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier;
            }

            return this.View(errorViewModel);
        }
    }
}

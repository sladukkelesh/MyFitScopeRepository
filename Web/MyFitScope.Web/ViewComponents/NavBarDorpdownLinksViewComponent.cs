namespace MyFitScope.Web.ViewComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Services.Data;
    using MyFitScope.Web.ViewModels.NavBarDropdownLinks;

    [ViewComponent(Name = "DropdownLinks")]
    public class NavBarDorpdownLinksViewComponent : ViewComponent
    {
        private readonly INavbarDropdownLinksServices dropdownLinksServices;

        public NavBarDorpdownLinksViewComponent(INavbarDropdownLinksServices dropdownLinksServices)
        {
            this.dropdownLinksServices = dropdownLinksServices;
        }

        public IViewComponentResult Invoke(string enumName)
        {
            var model = new DropdownLinksViewModel
            {
                Categories = this.dropdownLinksServices.GetLinksCategories(enumName),
            };

            return this.View(model);
        }
    }
}

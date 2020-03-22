namespace MyFitScope.Services.Data
{
    using System.Collections.Generic;

    using MyFitScope.Web.ViewModels.NavBarDropdownLinks;

    public interface INavbarDropdownLinksServices
    {
        IEnumerable<LinkViewModel> GetLinksCategories(string enumName);
    }
}

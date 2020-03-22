namespace MyFitScope.Web.ViewModels.NavBarDropdownLinks
{
    public class LinkViewModel
    {
        public string CategoryType { get; set; }

        public string CategoryTitle
            => this.CategoryType.Replace("_", " ");
    }
}
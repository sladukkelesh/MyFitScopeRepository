namespace MyFitScope.Web.ViewModels.NavBarDropdownLinks
{
    public class LinkViewModel
    {
        public string CategoryType { get; set; }

        public string CategoryTitle
            => this.CategoryType.Replace("_", " ");

        public string Url { get; set; }

        public string FullUrl
            => this.Url + this.CategoryType;
    }
}
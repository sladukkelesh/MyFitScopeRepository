namespace MyFitScope.Web.ViewModels.BlogMenuArticlesCategories
{
    public class ArticleCategoryViewModel
    {
        public string CategoryType { get; set; }

        public string CategoryTitle
            => this.CategoryType.Replace("_", " ");
    }
}

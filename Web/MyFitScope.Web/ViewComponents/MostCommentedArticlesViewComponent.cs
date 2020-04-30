namespace MyFitScope.Web.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Services.Data.Component;
    using MyFitScope.Web.ViewModels.MostCommentedArticles;

    [ViewComponent(Name = "MostCommentedArticles")]
    public class MostCommentedArticlesViewComponent : ViewComponent
    {
        private readonly IMostCommentedArticlesServices mostCommentedArticlesServices;

        public MostCommentedArticlesViewComponent(IMostCommentedArticlesServices mostCommentedArticlesServices)
        {
            this.mostCommentedArticlesServices = mostCommentedArticlesServices;
        }

        public IViewComponentResult Invoke()
        {
            var model = new MostCommentedArticlesLinksViewModel
            {
                Links = this.mostCommentedArticlesServices.GetMostCommentedArticles(),
            };

            return this.View(model);
        }
    }
}

namespace MyFitScope.Services.Data.Component
{
    using System.Collections.Generic;
    using System.Linq;

    using MyFitScope.Data.Common.Repositories;
    using MyFitScope.Data.Models.BlogModels;
    using MyFitScope.Services.Mapping;
    using MyFitScope.Web.ViewModels.MostCommentedArticles;

    public class MostCommentedArticlesServices : IMostCommentedArticlesServices
    {
        private readonly IDeletableEntityRepository<Article> articlesRepository;

        public MostCommentedArticlesServices(IDeletableEntityRepository<Article> articlesRepository)
        {
            this.articlesRepository = articlesRepository;
        }

        public IEnumerable<MostCommentedArticleViewModel> GetMostCommentedArticles()
               => this.articlesRepository.All()
                                .Where(a => a.Comments.Count > 0)
                                .OrderByDescending(a => a.Comments.Count)
                                .To<MostCommentedArticleViewModel>()
                                .Take(5)
                                .ToList();
    }
}

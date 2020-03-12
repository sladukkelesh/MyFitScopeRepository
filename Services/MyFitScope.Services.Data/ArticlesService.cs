namespace MyFitScope.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MyFitScope.Data.Common.Repositories;
    using MyFitScope.Data.Models.BlogModels;
    using MyFitScope.Services.Mapping;
    using MyFitScope.Web.ViewModels.Articles;

    public class ArticlesService : IArticlesService
    {
        private readonly IDeletableEntityRepository<Article> articlesRepository;

        public ArticlesService(IDeletableEntityRepository<Article> articlesRepository)
        {
            this.articlesRepository = articlesRepository;
        }

        public IEnumerable<ArticleViewModel> GetAllArticles()
                    => this.articlesRepository.All()
                        .To<ArticleViewModel>()
                        .ToList();

        public DetailsArticleViewModel GetArticleById(string articleId)
                    => this.articlesRepository.All()
                            .Where(a => a.Id == articleId)
                            .To<DetailsArticleViewModel>()
                            .FirstOrDefault();
    }
}

namespace MyFitScope.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyFitScope.Data.Common.Repositories;
    using MyFitScope.Data.Models.BlogModels;
    using MyFitScope.Data.Models.BlogModels.Contracts;
    using MyFitScope.Services.Mapping;
    using MyFitScope.Web.ViewModels.Articles;

    public class ArticlesService : IArticlesService
    {
        private readonly IDeletableEntityRepository<Article> articlesRepository;

        public ArticlesService(IDeletableEntityRepository<Article> articlesRepository)
        {
            this.articlesRepository = articlesRepository;
        }

        public async Task CreateArticle(string articleTitle, ArticleCategory articleCategory, string articleImageUrl, string articleContent, string userId)
        {
            var article = new Article
            {
                Title = articleTitle,
                ArticleCategory = articleCategory,
                ImageUrl = articleImageUrl,
                Content = articleContent,
                UserId = userId,
            };

            await this.articlesRepository.AddAsync(article);
            await this.articlesRepository.SaveChangesAsync();
        }

        public async Task DeleteArticleAsync(string articleId)
        {
            var articleToDelete = await this.articlesRepository.GetByIdWithDeletedAsync(articleId);

            this.articlesRepository.HardDelete(articleToDelete);
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

        public IEnumerable<ArticleViewModel> GetArticlesByCategory(string articleCategoryInput)
                    => this.articlesRepository.All()
                    .Where(a => a.ArticleCategory == (ArticleCategory)Enum.Parse(typeof(ArticleCategory), articleCategoryInput))
                    .To<ArticleViewModel>()
                    .ToList();
    }
}

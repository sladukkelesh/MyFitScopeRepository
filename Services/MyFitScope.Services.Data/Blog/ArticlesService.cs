namespace MyFitScope.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using MyFitScope.Common;
    using MyFitScope.Data.Common.Repositories;
    using MyFitScope.Data.Models.BlogModels;
    using MyFitScope.Data.Models.BlogModels.Enums;
    using MyFitScope.Services.Mapping;
    using MyFitScope.Web.Infrastructure;
    using MyFitScope.Web.ViewModels.Articles;

    public class ArticlesService : IArticlesService
    {
        private const string InvalidCloudinaryResponseParams = "Missing response from Cloudinary!";
        private const string InvalidArticleIdErrorMessage = "Article with ID: {0} does not exist.";

        private readonly IDeletableEntityRepository<Article> articlesRepository;
        private readonly ICloudinaryService cloudinaryService;

        public ArticlesService(IDeletableEntityRepository<Article> articlesRepository, ICloudinaryService cloudinaryService)
        {
            this.articlesRepository = articlesRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public bool ArticleTitleAlreadyExists(string title)
        {
            return this.articlesRepository.All().Any(e => e.Title == title);
        }

        public async Task<string> CreateArticle(string articleTitle, ArticleCategory articleCategory, string articleContent, string userId, IFormFile photo)
        {
            var uploadPhotoResponse = await this.cloudinaryService.UploadPhotoAsync(photo, articleTitle.Replace(" ", "_") + "_image", GlobalConstants.CloudArticlesImageFolder);

            if (uploadPhotoResponse == null)
            {
                throw new ArgumentNullException(InvalidCloudinaryResponseParams);
            }

            var article = new Article
            {
                Title = articleTitle,
                ArticleCategory = articleCategory,
                ImagePublicId = uploadPhotoResponse.PublicId,
                ImageUrl = uploadPhotoResponse.PhotoUrl,
                Content = articleContent,
                UserId = userId,
            };

            await this.articlesRepository.AddAsync(article);
            await this.articlesRepository.SaveChangesAsync();

            return article.Id;
        }

        public async Task DeleteArticleAsync(string articleId)
        {
            var articleToDelete = await this.articlesRepository.GetByIdWithDeletedAsync(articleId);

            if (articleToDelete == null)
            {
                throw new ArgumentNullException(
                    string.Format(InvalidArticleIdErrorMessage, articleId));
            }

            articleToDelete.IsDeleted = true;

            await this.articlesRepository.SaveChangesAsync();
        }

        public T GetArticleById<T>(string articleId)
        {
            var article = this.articlesRepository.All()
                            .Where(a => a.Id == articleId)
                            .To<T>()
                            .FirstOrDefault();

            if (article == null)
            {
                throw new ArgumentNullException(
                    string.Format(InvalidArticleIdErrorMessage, articleId));
            }

            return article;
        }

        public async Task<PaginatedList<ArticleViewModel>> GetArticlesByCategoryAsync(string articleCategoryInput, int? pageIndex = null)
        {
            var result = this.articlesRepository.All();

            if (articleCategoryInput != "All")
            {
                    result = result.Where(a => a.ArticleCategory == (ArticleCategory)Enum.Parse(typeof(ArticleCategory), articleCategoryInput));
            }

            return await PaginatedList<ArticleViewModel>.CreateAsync(result.To<ArticleViewModel>(), pageIndex ?? GlobalConstants.PaginationDefaultPageIndex, GlobalConstants.PaginationPageSize);
        }

        public async Task<PaginatedList<ArticleViewModel>> GetArticlesByKeyWordAsync(string keyWord = null, int? pageIndex = null)
        {
            var result = this.articlesRepository.All();

            result = result.Where(a => a.Title.ToLower().Contains(keyWord.ToLower()));

            return await PaginatedList<ArticleViewModel>.CreateAsync(result.To<ArticleViewModel>(), pageIndex ?? GlobalConstants.PaginationDefaultPageIndex, GlobalConstants.PaginationPageSize);
        }

        public async Task UpdateArticleAsync(string articleId, string articleTitle, ArticleCategory articleCategory, IFormFile photo, string articleContent)
        {
            var articleToUpdate = await this.articlesRepository.GetByIdWithDeletedAsync(articleId);

            if (articleToUpdate == null)
            {
                throw new ArgumentNullException(
                    string.Format(InvalidArticleIdErrorMessage, articleId));
            }

            if (photo != null)
            {
                this.cloudinaryService.DeletePhoto(articleToUpdate.ImagePublicId);

                var uploadPhotoResponse = await this.cloudinaryService.UploadPhotoAsync(photo, articleTitle.Replace(" ", "_") + "_image", GlobalConstants.CloudUsersImageFolder);

                if (uploadPhotoResponse == null)
                {
                    throw new ArgumentNullException(InvalidCloudinaryResponseParams);
                }

                articleToUpdate.ImageUrl = uploadPhotoResponse.PhotoUrl;
                articleToUpdate.ImagePublicId = uploadPhotoResponse.PublicId;
            }

            articleToUpdate.Title = articleTitle;
            articleToUpdate.ArticleCategory = articleCategory;
            articleToUpdate.Content = articleContent;
            articleToUpdate.ModifiedOn = DateTime.UtcNow;

            this.articlesRepository.Update(articleToUpdate);
            await this.articlesRepository.SaveChangesAsync();
        }
    }
}

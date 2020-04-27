namespace MyFitScope.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Moq;
    using MyFitScope.Data.Common.Repositories;
    using MyFitScope.Data.Models.BlogModels;
    using MyFitScope.Data.Models.BlogModels.Enums;
    using MyFitScope.Data.Repositories;
    using MyFitScope.Services.Data.Tests.Common;
    using MyFitScope.Web.ViewModels.Cloudinary;
    using Xunit;

    public class ArticlesServiceTests
    {
        [Theory]
        [InlineData("Some Title")]
        [InlineData("Second Title")]
        public void TestArticleTitleAlreadyExists_WithInlineData_ShouldReturnTrue(string data)
        {
            var repository = new Mock<IDeletableEntityRepository<Article>>();
            repository.Setup(r => r.All()).Returns(new List<Article>()
                                                        {
                                                            new Article { Title = "Some Title" },
                                                            new Article { Title = "Second Title" },
                                                        }.AsQueryable());

            var service = new ArticlesService(repository.Object, new ImageService());
            Assert.True(service.ArticleTitleAlreadyExists(data));
        }

        [Theory]
        [InlineData("Missing Title1")]
        [InlineData("")]
        public void TestArticleTitleAlreadyExists_WithNonExistingData_ShouldReturnFalse(string data)
        {
            var repository = new Mock<IDeletableEntityRepository<Article>>();
            repository.Setup(r => r.All()).Returns(new List<Article>()
                                                        {
                                                            new Article { Title = "Some Title" },
                                                            new Article { Title = "Second Title" },
                                                        }.AsQueryable());

            var service = new ArticlesService(repository.Object, new ImageService());
            Assert.False(service.ArticleTitleAlreadyExists(data));
        }

        [Fact]
        public async Task TestUpdateArticleAsync_WithValidData_ShouldUpdateCorrectly()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var repository = new EfDeletableEntityRepository<Article>(context);

            var articleId = "asdf";
            await repository.AddAsync(new Article { Id = articleId });
            await repository.SaveChangesAsync();

            var service = new ArticlesService(repository, new ImageService());
            await service.UpdateArticleAsync(articleId, "Mama", ArticleCategory.Fitness, null, "Some Content");

            var updatedArticle = repository.All().Where(a => a.Id == articleId).FirstOrDefault();

            Assert.Equal("Mama", updatedArticle.Title);
            Assert.Equal("Some Content", updatedArticle.Content);
        }

        [Fact]
        public async Task TestDeleteArticleAsync_WithCorrectData_ShouldSetIsDeletedToTrue()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var repository = new EfDeletableEntityRepository<Article>(context);

            var articleId = "asdf";
            await repository.AddAsync(new Article { Id = articleId });
            await repository.SaveChangesAsync();

            var service = new ArticlesService(repository, new ImageService());

            await service.DeleteArticleAsync(articleId);

            var articlesCount = repository.All().ToList().Count;

            Assert.Equal(0, articlesCount);
        }

        private class ImageService : ICloudinaryService
        {
            public void DeletePhoto(string publicId)
            {
            }

            public Task<CloudinaryResultModel> UploadPhotoAsync(IFormFile file, string fileName, string folder)
            {
                return null;
            }
        }
    }
}

namespace MyFitScope.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MyFitScope.Common;
    using MyFitScope.Data.Models.BlogModels;
    using MyFitScope.Data.Models.BlogModels.Enums;

    internal class ArticlesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Articles.Any())
            {
                return;
            }

            var userId = dbContext.Users.FirstOrDefault().Id;

            for (int i = 1; i <= GlobalConstants.ArticlesEntitiesCount; i++)
            {
                await dbContext.Articles.AddAsync(new Article
                {
                    UserId = userId,
                    ArticleCategory = (ArticleCategory)i,
                    Title = $"Article {i}",
                    Content = GlobalConstants.ArticleContent,
                    ImageUrl = GlobalConstants.ArticleImageUrl,

                    // To add publicId manually --> "{image folder(on Cloudinary)}/{image name}.{image extension}"!!!
                    ImagePublicId = GlobalConstants.ArticleImagePublicId,
                });
            }
        }
    }
}

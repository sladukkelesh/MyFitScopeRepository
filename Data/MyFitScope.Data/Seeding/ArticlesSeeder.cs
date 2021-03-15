namespace MyFitScope.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MyFitScope.Data.Models.BlogModels;
    using MyFitScope.Data.Models.BlogModels.Enums;

    using static MyFitScope.Common.GlobalConstants;

    internal class ArticlesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Articles.Any())
            {
                return;
            }

            var userId = dbContext.Users.FirstOrDefault().Id;

            for (int i = 1; i <= EntitiesCount; i++)
            {
                await dbContext.Articles.AddAsync(new Article
                {
                    UserId = userId,
                    ArticleCategory = (ArticleCategory)i,
                    Title = $"Article {i}",
                    Content = $"Content for Article {i}",
                    ImageUrl = "https://res.cloudinary.com/myfitscope-cloud/image/upload/v1615231683/data_seeder/Some_public_ID.jpg",

                    // To add publicId manually --> "image_folder(on Cloudinary)/Image_name.(image extension)"!!!
                    ImagePublicId = "data_seeder/Some_public_ID.jpg",
                });
            }
        }
    }
}

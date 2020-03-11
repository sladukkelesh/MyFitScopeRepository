namespace MyFitScope.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyFitScope.Data.Models.BlogModels;

    public class ArticleSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Articles.Any())
            {
                return;
            }

            var articleTitles = new List<string>
            {
                "Big Muscles",
                "Heavy Dumbbells",
                "Heavy Lifting",
                "Healthy Food",
                "Eat more Vegetables",
                "Use External Vitamins",
                "Choose our grate shoes",
                "Sport Clothing just for You",
                "Every sport outfit you need",
            };

            foreach (var title in articleTitles)
            {
                await dbContext.Articles.AddAsync(new Article
                {
                    Title = title,
                    UserId = "949c08e0-2ac8-4b37-882c-65c9a38b64f2",
                    Content = title + " Some very logn content created just for testing",
                });
            }
        }
    }
}

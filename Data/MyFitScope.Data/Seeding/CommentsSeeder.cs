namespace MyFitScope.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyFitScope.Data.Models.BlogModels;

    internal class CommentsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Comments.Any())
            {
                return;
            }

            var userId = dbContext.Users.FirstOrDefault().Id;

            var articles = dbContext.Articles.ToArray();

            foreach (var article in articles)
            {
                await dbContext.Comments.AddAsync(new Comment
                {
                    ArticleId = article.Id,
                    UserId = userId,
                    Content = "Test content for this comment.",
                });
            }
        }
    }
}

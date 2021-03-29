namespace MyFitScope.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MyFitScope.Common;
    using MyFitScope.Data.Models.BlogModels;

    internal class ResponsesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Responses.Any())
            {
                return;
            }

            var userId = dbContext.Users.FirstOrDefault().Id;
            var articles = dbContext.Articles.ToArray();

            foreach (var article in articles)
            {
                var comments = dbContext.Comments.Where(c => c.ArticleId == article.Id).ToArray();

                foreach (var comment in comments)
                {
                    await dbContext.Responses.AddAsync(new Response
                    {
                        UserId = userId,
                        ArticleId = article.Id,
                        CommentId = comment.Id,
                        Content = GlobalConstants.ResponseContent,
                    });
                }
            }
        }
    }
}

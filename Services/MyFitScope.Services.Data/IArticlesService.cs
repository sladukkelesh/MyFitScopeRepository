﻿namespace MyFitScope.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyFitScope.Data.Models.BlogModels.Enums;
    using MyFitScope.Web.ViewModels.Articles;

    public interface IArticlesService
    {
        Task<string> CreateArticle(string articleTitle, ArticleCategory articleCategory, string articleImageUrl, string articleContent, string userId);

        IEnumerable<ArticleViewModel> GetArticlesByCategory(string articleCategory);

        T GetArticleById<T>(string articleId);

        Task DeleteArticleAsync(string articleId);

        Task UpdateArticleAsync(string articleId, string articleTitle, ArticleCategory articleCategory, string articleImageUrl, string articleContent);
    }
}

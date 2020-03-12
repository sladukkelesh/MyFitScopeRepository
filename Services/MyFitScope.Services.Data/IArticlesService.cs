﻿namespace MyFitScope.Services.Data
{
    using System.Collections.Generic;

    using MyFitScope.Web.ViewModels.Articles;

    public interface IArticlesService
    {
        IEnumerable<ArticleViewModel> GetAllArticles();

        DetailsArticleViewModel GetArticleById(string articleId);
    }
}

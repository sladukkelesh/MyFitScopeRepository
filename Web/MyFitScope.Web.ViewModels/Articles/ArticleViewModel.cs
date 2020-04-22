namespace MyFitScope.Web.ViewModels.Articles
{
    using System;
    using System.Net;
    using System.Text.RegularExpressions;

    using MyFitScope.Data.Models.BlogModels;
    using MyFitScope.Services.Mapping;

    public class ArticleViewModel : IMapFrom<Article>
    {
        public string Id { get; set; }

        public string UserUserName { get; set; }

        public DateTime CretedOn { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ShortContent
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Content, @"<[^>]+>", string.Empty));

                if (content.Length > 100)
                {
                    return content.Substring(0, 100) + "...";
                }

                return content;
            }
        }

        public string ImageUrl { get; set; }
    }
}

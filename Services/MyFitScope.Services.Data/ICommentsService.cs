namespace MyFitScope.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task Create(string commentContent, string articleId, string userId);
    }
}

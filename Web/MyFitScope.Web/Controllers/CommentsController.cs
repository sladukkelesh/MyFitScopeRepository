namespace MyFitScope.Web.Controllers
{
    using System;
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;
    using MyFitScope.Services.Data;

    public class CommentsController : BaseController
    {
        private readonly ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }
    }
}

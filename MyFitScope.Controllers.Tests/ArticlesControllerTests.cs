using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyFitScope.Data.Models;
using MyFitScope.Services.Data;
using MyFitScope.Web.Controllers;
using MyFitScope.Web.Infrastructure;
using MyFitScope.Web.ViewModels.Articles;
using System.Collections.Generic;
using Xunit;

namespace MyFitScope.Controllers.Tests
{
    public class ArticlesControllerTests
    {
        [Fact]
        public void TestViewModelForDetailsView()
        {
            var userManager = this.GetMockUserManager();

            var mockService = new Mock<IArticlesService>();
            mockService.Setup(x => x.GetArticleById<DetailsArticleViewModel>("111")).Returns(() =>
            {
                return new DetailsArticleViewModel { Comments = null, Title = "Mama", Content = "Mama" };
            });

            var controller = new ArticlesController(mockService.Object, userManager.Object);

            var articleId = "111";

            var result = controller.Details(articleId);

            Assert.IsType<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.IsType<DetailsArticleViewModel>(viewResult.Model);
            var viewModel = viewResult.Model as DetailsArticleViewModel;
            Assert.Equal("Mama", viewModel.Title);
            Assert.Equal("Mama", viewModel.Content);
        }

        private Mock<UserManager<ApplicationUser>> GetMockUserManager()
        {
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            return new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
        }

    }
}

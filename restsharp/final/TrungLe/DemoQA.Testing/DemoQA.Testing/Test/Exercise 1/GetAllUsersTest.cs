using FluentAssertions;
using DemoQA.Core.Extensions;
using DemoQA.Core.ExtentReport;
using DemoQA.Service.Services;


namespace DemoQA.Testing.Test.Exersize1
{
    [TestFixture, Category("GetAllUsers")]
    public class GetAllUsersTest : BaseTest
    {
        private UserService _userService;

        public GetAllUsersTest()
        {
            _userService = new UserService(ApiClient);
        }

        [Test]
        public async Task GetAllUsers()
        {
            ReportLog.Info("1. Get All Users");
            var response = await _userService.GetAllUsers();

            ReportLog.Info("2. Verify status code");
            response.VerifyStatusCodeOK();

            var result = response.Data;

            var total = result.Meta.Pagination.Total.ToString();
            var pages = result.Meta.Pagination.Pages.ToString();
            var page = result.Meta.Pagination.Page.ToString();
            var limit = result.Meta.Pagination.Limit.ToString();

            ReportLog.Info("3. Verify properties");
            total.Should().NotBeEmpty();
            pages.Should().NotBeEmpty();
            page.Should().NotBeEmpty();
            limit.Should().NotBeEmpty();
        }

    }
}

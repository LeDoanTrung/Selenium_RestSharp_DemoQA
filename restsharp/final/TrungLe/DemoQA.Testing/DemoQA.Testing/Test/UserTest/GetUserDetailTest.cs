using DemoQA.Core.Extensions;
using DemoQA.Core.ExtentReport;
using DemoQA.Service.DataObjects;
using DemoQA.Service.DataProvider;
using DemoQA.Service.Services;
using DemoQA.Testing.Constants;
using FluentAssertions;
using Newtonsoft.Json;

namespace DemoQA.Testing.Test.UserTest
{
    [TestFixture, Category("GetUserDetail")]
    public class GetUserDetailTest : BaseTest
    {
        private UserService _userService;
        private BookService _bookService;
        public GetUserDetailTest()
        {
            _userService = new UserService(ApiClient);
            _bookService = new BookService(ApiClient);
        }

        [Test]
        [TestCase("user_1")]
        public async Task GetUserDetailSuccessfullyWithValidData(string accountKey)
        {
            AccountDTO account = AccountData[accountKey];

            ReportLog.Info("Get Token");
            await _userService.StoreTokenAsync(accountKey, account);

            ReportLog.Info("Clear all book from collection");
            await _bookService.DeleteAllBooksFromCollectionAsync(account.Id, _userService.GetToken(accountKey));

            ReportLog.Info("Send request to get user detail with valid token");
            var response = await _userService.GetUserDetailAsync(account.Id, _userService.GetToken(accountKey));

            ReportLog.Info("Verify status code and user data");
            response.VerifyStatusCodeOK();
            await response.VerifySchema(FileConstant.GetUserDetailSchemaPath);
            response.Data.UserId.Should().Be(account.Id);
            response.Data.Username.Should().Be(account.UserName);
            response.Data.Books.Should().BeEmpty();
        }

        [Test]
        [TestCase("user_1")]
        public async Task GetUserDetailUnsuccessfullyWithInvalidToken(string accountKey)
        {
            AccountDTO account = AccountData[accountKey];

            ReportLog.Info("Get Token");
            await _userService.StoreTokenAsync(accountKey, account);

            ReportLog.Info("Clear all book from collection");
            await _bookService.DeleteAllBooksFromCollectionAsync(account.Id, _userService.GetToken(accountKey));

            ReportLog.Info("Send request to get user detail with invalid token");
            string invalidToken = UserDataProvider.GenerateInvalidToken();
            var response = await _userService.GetUserDetailAsync(account.Id, invalidToken);

            ReportLog.Info("Verify status code and message");
            response.VerifyStatusCodeUnauthorized();
            var result = (dynamic)JsonConvert.DeserializeObject(response.Content);
            ((string)result["message"]).Should().Be(MessageConstant.UserUnauthorizedMessage);
        }
    }
}

using DemoQA.Core.Extensions;
using DemoQA.Core.ExtentReport;
using DemoQA.Service.DataObjects;
using DemoQA.Service.DataProvider;
using DemoQA.Service.Services;
using DemoQA.Testing.Constants;
using FluentAssertions;
using Newtonsoft.Json;

namespace DemoQA.Testing.Test
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

            ReportLog.Info("1.  Get Token");
            await _userService.StoreTokenAsync(accountKey, account);

            ReportLog.Info("2. Clear all book from collection");
            var deleteBooksResponse = await _bookService.DeleteAllBooksFromCollectionAsync(account.Id, _userService.GetToken(accountKey));
            deleteBooksResponse.VerifyStatusCodeNoContent();

            ReportLog.Info("3. Send request to get user detail with valid token");
            var response = await _userService.GetUserDetailAsync(account.Id, _userService.GetToken(accountKey));

            ReportLog.Info("4. Verify status code and user data");
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

            ReportLog.Info("1. Get Token");
            await _userService.StoreTokenAsync(accountKey, account);

            ReportLog.Info("2. Clear all book from collection");
            var deleteBooksResponse = await _bookService.DeleteAllBooksFromCollectionAsync(account.Id, _userService.GetToken(accountKey));
            deleteBooksResponse.VerifyStatusCodeNoContent();

            ReportLog.Info("3. Send request to get user detail with invalid token");
            string invalidToken = UserDataProvider.GenerateInvalidToken();
            var response = await _userService.GetUserDetailAsync(account.Id, invalidToken);

            ReportLog.Info("4. Verify status code and message");
            response.VerifyStatusCodeUnauthorized();
            var result = (dynamic)JsonConvert.DeserializeObject(response.Content);
            ((string)result["message"]).Should().Be("User not authorized!");
        }
    }
}

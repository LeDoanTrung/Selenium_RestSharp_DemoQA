using DemoQA.Core.Extensions;
using DemoQA.Core.ExtentReport;
using DemoQA.Core.Util;
using DemoQA.Service.DataObjects;
using DemoQA.Service.Services;
using DemoQA.Testing.Constants;
using FluentAssertions;
using Newtonsoft.Json;


namespace DemoQA.Testing.Test.BookTest
{
    [TestFixture, Category("DeleteBookFromCollection")]
    [Parallelizable(ParallelScope.Fixtures)]
    public class DeleteBookFromCollectionTest : BaseTest
    {
        private UserService _userService;
        private BookService _bookService;

        [SetUp]
        public void SetUp()
        {
            _userService = new UserService(ApiClient);
            _bookService = new BookService(ApiClient);
        }

        [Test]
        [TestCase("test_user", "git_book")]
        public async Task DeleteExistingBookFromCollectionSuccessfully(string accountKey, string bookKey)
        {
            AccountDTO account = AccountData[accountKey];
            BookDTO book = BookData[bookKey];

            ReportLog.Info("Get Token");
            await _userService.StoreTokenAsync(accountKey, account);
            string token = _userService.GetToken(accountKey);

            ReportLog.Info("Clear book from collection");
            await _bookService.DeleteBookFromCollectionAsync(account.Id, book.Isbn, token);

            ReportLog.Info("Send request to add new book to collection");
            await _bookService.AddBookToCollectionAsync(account.Id, book.Isbn, token);

            ReportLog.Info("Delete an esisting book from collection");
            var response = await _bookService.DeleteBookFromCollectionAsync(account.Id, book.Isbn, token);

            ReportLog.Info("Verify status code");
            response.VerifyStatusCodeNoContent();
        }

        [Test]
        [TestCase("test_user", "git_book")]
        public async Task DeleteNonExistingBookFromCollectionUnsuccessfully(string accountKey, string bookKey)
        {
            AccountDTO account = AccountData[accountKey];
            BookDTO book = BookData[bookKey];

            ReportLog.Info("Get Token");
            await _userService.StoreTokenAsync(accountKey, account);
            string token = _userService.GetToken(accountKey);

            ReportLog.Info("Clear book from collection");
            await _bookService.DeleteBookFromCollectionAsync(account.Id, book.Isbn, token);

            ReportLog.Info("Delete a non-existing book from collection");
            var response = await _bookService.DeleteBookFromCollectionAsync(account.Id, book.Isbn, token);

            ReportLog.Info("Verify status code");
            response.VerifyStatusCodeBadRequest();
            var result = (dynamic)JsonConvert.DeserializeObject(response.Content);
            ((string)result["message"]).Should().Be(MessageConstant.NonExistingInUserCollectionMessage);
        }
    }
}

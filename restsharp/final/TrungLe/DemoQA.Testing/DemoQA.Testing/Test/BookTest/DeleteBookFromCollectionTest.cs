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
    public class DeleteBookFromCollectionTest : BaseTest
    {
        private UserService _userService;
        private BookService _bookService;
        private Dictionary<string, BookDTO> BookData = JsonFileUtility.ReadAndParse<Dictionary<string, BookDTO>>(FileConstant.BookFilePath.GetAbsolutePath());
        public DeleteBookFromCollectionTest()
        {
            _userService = new UserService(ApiClient);
            _bookService = new BookService(ApiClient);
        }

        [Test]
        [TestCase("user_1", "book_1")]
        public async Task DeleteExistingBookFromCollectionSuccessfully(string accountKey, string bookKey)
        {
            AccountDTO account = AccountData[accountKey];
            BookDTO book = BookData[bookKey];

            ReportLog.Info("Get Token");
            await _userService.StoreTokenAsync(accountKey, account);
            string token = _userService.GetToken(accountKey);

            ReportLog.Info("Clear book from collection");
            await _bookService.DeleteBookFromCollection(account.Id, book.Isbn, token);

            ReportLog.Info("Send request to add new book to collection");
            await _bookService.AddBookToCollection(account.Id, book.Isbn, token);

            ReportLog.Info("Delete an esisting book from collection");
            var response = await _bookService.DeleteBookFromCollection(account.Id, book.Isbn, token);

            ReportLog.Info("Verify status code");
            response.VerifyStatusCodeNoContent();
        }

        [Test]
        [TestCase("user_1", "book_1")]
        public async Task DeleteNonExistingBookFromCollectionSuccessfully(string accountKey, string bookKey)
        {
            AccountDTO account = AccountData[accountKey];
            BookDTO book = BookData[bookKey];

            ReportLog.Info("Get Token");
            await _userService.StoreTokenAsync(accountKey, account);
            string token = _userService.GetToken(accountKey);

            ReportLog.Info("Clear book from collection");
            await _bookService.DeleteBookFromCollection(account.Id, book.Isbn, token);

            ReportLog.Info("Delete a non-existing book from collection");
            var response = await _bookService.DeleteBookFromCollection(account.Id, book.Isbn, token);

            ReportLog.Info("Verify status code");
            response.VerifyStatusCodeBadRequest();
            var result = (dynamic)JsonConvert.DeserializeObject(response.Content);
            ((string)result["message"]).Should().Be(MessageConstant.DeleteNonExistingBookMessage);
        }
    }
}

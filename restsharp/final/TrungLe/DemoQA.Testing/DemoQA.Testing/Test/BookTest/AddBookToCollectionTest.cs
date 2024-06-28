using DemoQA.Core.Extensions;
using DemoQA.Core.ExtentReport;
using DemoQA.Core.Util;
using DemoQA.Service.DataObjects;
using DemoQA.Service.DataProvider;
using DemoQA.Service.Services;
using DemoQA.Testing.Constants;
using FluentAssertions;
using Newtonsoft.Json;

namespace DemoQA.Testing.Test.BookTest
{
    [TestFixture, Category("AddBookToCollection")]
    [Parallelizable(ParallelScope.Fixtures)]
    public class AddBookToCollectionTest : BaseTest
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
        [TestCase("user_1", "book_1")]
        public async Task AddBookToCollectionSuccessfully(string accountKey, string bookKey)
        {
            AccountDTO account = AccountData[accountKey];
            BookDTO book = BookData[bookKey];

            ReportLog.Info("Get Token");
            await _userService.StoreTokenAsync(accountKey, account);
            string token = _userService.GetToken(accountKey);

            ReportLog.Info("Clear book from collection");
            await _bookService.DeleteBookFromCollectionAsync(account.Id, book.Isbn, token);

            ReportLog.Info("Send request to add new book to collection");
            var response = await _bookService.AddBookToCollectionAsync(account.Id, book.Isbn, token);

            ReportLog.Info("Verify status code and body");
            response.VerifyStatusCodeCreated();
            _bookService.StoreDataToDeleteBook(account.Id, book.Isbn, token);
            await response.VerifySchema(FileConstant.AddBookToCollectionlSchemaPath);

            var result = response.Data;
            result.Books.Should().NotBeNull();
            result.Books.Should().ContainSingle(b => b.Isbn == book.Isbn);
        }

        [Test]
        [TestCase("user_1", "book_1")]
        public async Task AddDuplicatedBookToCollectionUnsuccessfully(string accountKey, string bookKey)
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
            _bookService.StoreDataToDeleteBook(account.Id, book.Isbn, token);

            ReportLog.Info("Send request to add duplciated book to collection");
            var response = await _bookService.AddBookToCollectionAsync(account.Id, book.Isbn, token);

            ReportLog.Info("Verify status code and message");
            response.VerifyStatusCodeBadRequest();
            var result = (dynamic)JsonConvert.DeserializeObject(response.Content);
            ((string)result["message"]).Should().Be(MessageConstant.DuplicatedBookMessage);
        }


        [TearDown]
        public void AfterAddBookTest()
        {
            ReportLog.Info("Clear book");
            _bookService.DeleteCreatedBookFromStorage();
        }
    }
}

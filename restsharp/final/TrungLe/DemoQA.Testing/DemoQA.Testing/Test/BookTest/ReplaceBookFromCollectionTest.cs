﻿using DemoQA.Core.Extensions;
using DemoQA.Core.ExtentReport;
using DemoQA.Core.Util;
using DemoQA.Service.DataObjects;
using DemoQA.Service.DataProvider;
using DemoQA.Service.Models.Response;
using DemoQA.Service.Services;
using DemoQA.Testing.Constants;
using FluentAssertions;
using Newtonsoft.Json;


namespace DemoQA.Testing.Test.BookTest
{
    [TestFixture, Category("ReplaceBookFromCollection")]
    [Parallelizable(ParallelScope.Fixtures)]
    public class ReplaceBookFromCollectionTest : BaseTest
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
        [TestCase("test_user", "git_book", "javascript_book")]
        public async Task ReplaceWithExistingBookFromCollectionSuccessfully(string accountKey, string isbnReplaced, string isbnNew)
        {
            AccountDTO account = AccountData[accountKey];
            BookDTO bookReplaced = BookData[isbnReplaced];
            BookDTO bookNew = BookData[isbnNew];

            ReportLog.Info("Get Token");
            await _userService.StoreTokenAsync(accountKey, account);
            string token = _userService.GetToken(accountKey);

            ReportLog.Info("Clear book from collection");
            await _bookService.DeleteBookFromCollectionAsync(account.Id, bookReplaced.Isbn, token);

            ReportLog.Info("Send request to add the book to be replaced to collection");
            await _bookService.AddBookToCollectionAsync(account.Id, bookReplaced.Isbn, token);

            ReportLog.Info("Send request to replace a book from collection");
            var response = await _bookService.ReplaceBookFromCollectionAsync(account.Id, bookReplaced.Isbn, bookNew.Isbn, token);

            ReportLog.Info("Verify status code and body");
            response.VerifyStatusCodeOK();
            _bookService.StoreDataToDeleteBook(account.Id, bookNew.Isbn, token);
            await response.VerifySchema(FileConstant.AddBookToCollectionlSchemaPath);

            var result = response.Data;
            result.Books.Should().NotContain(b => b.Isbn == bookReplaced.Isbn);
            result.Books.Should().ContainSingle(b => b.Isbn == bookNew.Isbn);

        }

        [Test]
        [TestCase("test_user", "git_book")]
        public async Task ReplaceWithNonExistingBookFromCollectionUnsuccessfully(string accountKey, string bookKey)
        {
            AccountDTO account = AccountData[accountKey];
            BookDTO book = BookData[bookKey];

            ReportLog.Info("Get Token");
            await _userService.StoreTokenAsync(accountKey, account);
            string token = _userService.GetToken(accountKey);

            ReportLog.Info("Clear book from collection");
            await _bookService.DeleteBookFromCollectionAsync(account.Id, book.Isbn, token);

            ReportLog.Info("Send request to add the book to be replaced to collection");
            await _bookService.AddBookToCollectionAsync(account.Id, book.Isbn, token);
            _bookService.StoreDataToDeleteBook(account.Id, book.Isbn, token);

            ReportLog.Info("Send request to replace a book from collection");
            string invalidIsbn = BookDataProvider.GenerateInvalidIsbn();
            var response = await _bookService.ReplaceBookFromCollectionAsync(account.Id, book.Isbn, invalidIsbn, token);

            ReportLog.Info("Verify status code");
            response.VerifyStatusCodeBadRequest();
            var result = (dynamic)JsonConvert.DeserializeObject(response.Content);
            ((string)result["message"]).Should().Be(MessageConstant.NonExistingInBookCollectionMessage);

        }

        [TearDown]
        public void AfterReplaceBookTest()
        {
            ReportLog.Info("Clear book");
            _bookService.DeleteCreatedBookFromStorage();
        }
    }
}

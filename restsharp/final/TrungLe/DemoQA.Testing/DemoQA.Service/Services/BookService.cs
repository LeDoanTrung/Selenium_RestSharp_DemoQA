using DemoQA.Core.API;
using DemoQA.Core.ShareData;
using DemoQA.Service.Models.Request;
using DemoQA.Service.Models.Response;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQA.Service.Services
{
    public class BookService
    {
        private readonly APIClient _client;

        public BookService(APIClient client)
        {
            _client = client;
        }

        public async Task<RestResponse> DeleteAllBooksFromCollectionAsync(string userId, string token)
        {
            return await _client.CreateRequest(APIConstant.DeleteAllBooksEndPoint)
                         .AddHeader("accept", ContentType.Json)
                         .AddParameter("UserId", userId)
                         .AddAuthorizationHeader(token)
                         .ExecuteDeleteAsync();
        }

        public async Task<RestResponse> DeleteBookFromCollection(string userId, string isbn, string token)
        {
            return await _client.CreateRequest(APIConstant.DeleteBookEndPoint)
                   .AddHeader("accept", ContentType.Json)
                   .AddHeader("Content-Type", ContentType.Json)
                   .AddAuthorizationHeader(token)
                   .AddBody(new DeleteBookRequestDTO
                   {
                       Isbn = isbn,
                       UserId = userId
                   }, ContentType.Json)
                   .ExecuteDeleteAsync();
        }

        public async Task<RestResponse<AddBookResponseDTO>> AddBookToCollection (string userId, string isbn, string token)
        {
            return await _client.CreateRequest(APIConstant.AddBookEndPoint)
                   .AddHeader("accept", ContentType.Json)
                   .AddHeader("Content-Type", ContentType.Json)
                   .AddAuthorizationHeader(token)
                   .AddBody(new AddBookRequestDTO
                   {
                       UserId = userId,
                       CollectionOfIsbns = new List<CollectionOfIsbn>
                       {
                           new CollectionOfIsbn
                           {
                               Isbn = isbn
                           }
                       }
                   }, ContentType.Json)
                   .ExecutePostAsync<AddBookResponseDTO>();
        }
        public static void StoreDataToDeleteBook(string userId, string isbn, string token)
        {
            DataStorage.SetData("hasCreatedBook", true);
            DataStorage.SetData("userId", userId);
            DataStorage.SetData("isbn", isbn);
            DataStorage.SetData("token", token);
        }

        public static void DeleteCreatedBookFromStorage(BookService bookService)
        {
            if ((Boolean)DataStorage.GetData("hasCreatedBook"))
            {
                bookService.DeleteBookFromCollection(
                (string)DataStorage.GetData("userId"),
                (string)DataStorage.GetData("isbn"),
                (string)DataStorage.GetData("token")
                );
            }
        }
    }
}

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

        public async Task<RestResponse> DeleteBookFromCollectionAsync(string userId, string isbn, string token)
        {
            return await _client.CreateRequest(APIConstant.DeleteBookEndPoint)
                   .AddHeader("accept", ContentType.Json)
                   .AddHeader("Content-Type", ContentType.Json)
                   .AddAuthorizationHeader(token)
                   .AddBody(new BookRequestDTO
                   {
                       Isbn = isbn,
                       UserId = userId
                   }, ContentType.Json)
                   .ExecuteDeleteAsync();
        }

        public async Task<RestResponse<AddBookResponseDTO>> AddBookToCollectionAsync(string userId, string isbn, string token)
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

        public async Task<RestResponse<UserResponseDTO>> ReplaceBookFromCollectionAsync(string userId, string isbnReplaced, string isbnNew, string token)
        {
            string endpoint = String.Format(APIConstant.ReplaceBookEndPoint, isbnReplaced);
            return await _client.CreateRequest(endpoint)
                   .AddHeader("accept", ContentType.Json)
                   .AddHeader("Content-Type", ContentType.Json)
                   .AddAuthorizationHeader(token)
                   .AddBody(new BookRequestDTO
                   {
                       Isbn = isbnNew,
                       UserId = userId
                   }, ContentType.Json)
                   .ExecutePutAsync<UserResponseDTO>();
        }

        public void StoreDataToDeleteBook(string userId, string isbn, string token)
        {
            DataStorage.SetData("hasCreatedBook", true);
            DataStorage.SetData("userId", userId);
            DataStorage.SetData("isbn", isbn);
            DataStorage.SetData("token", token);
        }

        public void DeleteCreatedBookFromStorage()
        {
            if ((Boolean)DataStorage.GetData("hasCreatedBook"))
            {
                this.DeleteBookFromCollectionAsync(
                (string)DataStorage.GetData("userId"),
                (string)DataStorage.GetData("isbn"),
                (string)DataStorage.GetData("token")
                );
            }
        }
    }
}

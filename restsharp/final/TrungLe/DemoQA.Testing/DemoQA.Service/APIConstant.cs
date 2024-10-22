﻿
namespace DemoQA.Service
{
    public class APIConstant
    {
        //BookStore Endpoint
        public const string AddBookEndPoint = "/BookStore/v1/Books";
        public const string DeleteBookEndPoint = "/BookStore/v1/Book";
        public const string ReplaceBookEndPoint = "/BookStore/v1/Books/{0}";
        public const string DeleteAllBooksEndPoint = "/BookStore/v1/Books";
        
        //Account Endpoint
        public const string GetUserDetailEndPoint = "/Account/v1/User/{0}";
        public const string GenerateTokenEndPoint = "/Account/v1/GenerateToken";
    }
}

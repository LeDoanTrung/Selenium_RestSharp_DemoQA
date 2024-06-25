using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQA.Testing.Constants
{
    public class FileConstant
    {
        //Test Data
        public const string AccountFilePath = @"TestData\Users\account_data.json";
        public const string BookFilePath = @"TestData\Books\book_data.json";

        //Schema
        public const string GetUserDetailSchemaPath = @"Resource\Schema\getUserDetail_schema.json";
        public const string AddBookToCollectionlSchemaPath = @"Resource\Schema\addBookToCollection_schema.json";
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQA.Service.Models.Response
{
    public class BookAdded
    {
        public string isbn { get; set; }
    }

    public class AddBookResponseDTO
    {
        public List<BookAdded> books { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQA.Service.Models.Response
{
    public class Book
    {
        [JsonProperty("isbn")]
        public string Isbn { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("subTitle")]
        public string SubTitle { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("publish_date")]
        public DateTime Publish_date { get; set; }

        [JsonProperty("publisher")]
        public string Publisher { get; set; }

        [JsonProperty("pages")]
        public int Pages { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }
    }

    public class UserResponseDTO
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("books")]
        public List<Book> Books { get; set; }
    }

}

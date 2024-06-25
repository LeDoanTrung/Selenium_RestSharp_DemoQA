using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQA.Service.Models.Request
{
    public class CollectionOfIsbn
    {
        [JsonProperty("isbn")]
        public string Isbn { get; set; }
    }

    public class AddBookRequestDTO
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("collectionOfIsbns")]
        public List<CollectionOfIsbn> CollectionOfIsbns { get; set; }
    }

}

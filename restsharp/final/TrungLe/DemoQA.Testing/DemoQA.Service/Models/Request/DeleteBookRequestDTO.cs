using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQA.Service.Models.Request
{
    public class DeleteBookRequestDTO
    {
        [JsonProperty("isbn")]
        public string Isbn { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}

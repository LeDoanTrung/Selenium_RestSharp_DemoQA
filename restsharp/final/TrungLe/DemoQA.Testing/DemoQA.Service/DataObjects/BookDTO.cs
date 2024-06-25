using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQA.Service.DataObjects
{
    public class BookDTO
    {
        [JsonProperty("isbn")]
        public string Isbn { get; set; }
    }
}

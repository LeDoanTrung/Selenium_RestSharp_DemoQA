using DemoQA.Service.Models.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQA.Service.DataObjects
{
    public class AccountDTO
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQA.Service.Models.Response
{
    public class DeleteUserResponseDTO
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("meta")]
        public object Meta { get; set; }

        [JsonProperty("data")]
        public object Data { get; set; }
    }
}

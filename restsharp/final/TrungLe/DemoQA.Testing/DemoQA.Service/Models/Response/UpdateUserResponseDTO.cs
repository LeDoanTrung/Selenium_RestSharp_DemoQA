using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQA.Service.Models.Response
{
    public class UpdateUserData
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("created_at")]
        public DateTime Created_at { get; set; }

        [JsonProperty("updated_at")]
        public DateTime Updated_at { get; set; }
    }

    public class UpdateUserResponseDTO
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("meta")]
        public object Meta { get; set; }

        [JsonProperty("data")]
        public UpdateUserData Data { get; set; }
    }
}

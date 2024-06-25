using Newtonsoft.Json;

namespace DemoQA.Service.Models.Request
{
    public class TokenRequestDTO
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}

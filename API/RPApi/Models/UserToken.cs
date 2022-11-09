using Newtonsoft.Json;
using System;

namespace RPApi.Models
{
    public class UserToken
    {
        [JsonProperty("Token")]
        public string Token { get; set; }

        [JsonProperty("Expiration")]
        public DateTime Expiration { get; set; }
    }
}

using Newtonsoft.Json;

namespace HackRHub.Models
{
    public class OAuthTokenResponse
    {
        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }
    }
}
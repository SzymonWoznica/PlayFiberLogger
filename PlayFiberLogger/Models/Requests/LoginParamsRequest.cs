using System.Text.Json.Serialization;

namespace PlayFiberLogger.Models.Requests
{
    public class LoginParamsRequest
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("URLIPAddress")]
        public string UrlIpAddress { get; set; }
    }
}

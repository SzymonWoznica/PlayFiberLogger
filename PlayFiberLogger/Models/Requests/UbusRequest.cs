using System.Text.Json.Serialization;

namespace PlayFiberLogger.Models.Requests
{
    public class UbusRequest<T>
    {
        [JsonPropertyName("jsonrpc")]
        public string JsonRpc { get; set; } = "2.0";

        [JsonPropertyName("id")]
        public int Id { get; set; } = 1;

        [JsonPropertyName("method")]
        public string Method { get; set; } = "call";

        [JsonPropertyName("params")]
        public object[] Params { get; set; }
    }
}

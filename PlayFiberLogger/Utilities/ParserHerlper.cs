using System.Text.Json;

namespace PlayFiberLogger.Utilities
{
    public static class ParserHerlper
    {
        public static string ParseSidFromJson(string jsonContent)
        {
            using var doc = JsonDocument.Parse(jsonContent);
            var root = doc.RootElement;
            return root.GetProperty("result")[1].GetProperty("ubus_rpc_session").GetString() ?? "";
        }
    }
}

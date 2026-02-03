using PlayFiberLogger.Utilities;

namespace Tests
{
    public class ParseSidFromJson
    {
        [Fact]
        public void ParseSid_ShouldReturnCorrectGuid_WhenJsonIsValid()
        {
            // Arrange (Przygotowanie danych wejściowych)
            var service = ParserHerlper;
            string fakeJson = @"{
            ""jsonrpc"": ""2.0"",
            ""result"": [0, { ""ubus_rpc_session"": ""069ff28c277bd755101ba04f664e6ace"" }]
        }";

            // Act (Wykonanie testowanej czynności)
            string resultSid = service.ParseSidFromJson(fakeJson);

            // Assert (Sprawdzenie czy wynik jest zgodny z oczekiwaniami)
            Assert.Equal("069ff28c277bd755101ba04f664e6ace", resultSid);
        }
    }
}
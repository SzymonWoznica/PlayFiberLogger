using PlayFiberLogger.Utilities;

namespace PlayFiberLogger.Tests
{
    public class ParserHelper
    {
        [Fact]
        public void ParseSid_ShouldReturnCorrectGuid_WhenJsonIsValid()
        {
            // Arrange
            string fakeJson = @"{
            ""jsonrpc"": ""2.0"",
            ""result"": [0, { ""ubus_rpc_session"": ""069ff28c277bd755101ba04f664e6ace"" }]
        }";

            // Act 
            var resultSid = ParserHerlper.ParseSidFromJson(fakeJson);

            // Assert 
            Assert.Equal("069ff28c277bd755101ba04f664e6ace", resultSid);
        }

        [Theory]
        [InlineData("FakeGuid")]
        [InlineData("25CF369903CC405B98D7A50E870CB281")]
        [InlineData("975C046EAA6D401C9A41D61F0080BA55")]
        public void ParseSid_ShouldReturnCorrectGuid_WhenJsonIsInvalid(string expectGuid)
        {
            // Arrange 
            string fakeJson = $@"{{
                ""jsonrpc"": ""2.0"",
                ""result"": [0, {{ ""ubus_rpc_session"": ""{expectGuid}"" }}]
            }}";

            // Act
            var resultSid = ParserHerlper.ParseSidFromJson(fakeJson);

            // Assert 
            Assert.NotEqual("069ff28c277bd755101ba04f664e6ace", resultSid);
        }
    }
}
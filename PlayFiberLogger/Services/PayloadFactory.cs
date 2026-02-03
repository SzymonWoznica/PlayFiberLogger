using PlayFiberLogger.Models.Requests;

namespace PlayFiberLogger.Services
{
    public static class PayloadFactory
    {
        public static UbusRequest<object> CreateLoginPayload(string user, string pass, string ip)
        {
            return new UbusRequest<object>
            {
                Params = new object[]
                {
                    "00000000000000000000000000000000",
                    "session",
                    "login",
                    new LoginParamsRequest
                    {
                        Username = user,
                        Password = pass,
                        UrlIpAddress = ip,
                    }
                }
            };
        }

        public static UbusRequest<object> CreateOntInfoPayload(string sid)
        {
            return new UbusRequest<object>
            {
                Id = 35, 
                Params = new object[]
                {
                    sid,
                    "xlink.gpon",
                    "OntInformationGet",
                    new 
                    { 
                        data = new { } 
                    }
                }
            };
        }
    }
}

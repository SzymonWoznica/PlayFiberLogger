using PlayFiberLogger.Models.Responses;
using PlayFiberLogger.Utilities;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace PlayFiberLogger.Services
{
    public sealed class RouterService
    {
        private static readonly Lazy<RouterService> _instance = new Lazy<RouterService>(() => new RouterService());

        public static RouterService Instance => _instance.Value;

        private readonly HttpClient _httpClient;

        public string CurrentSid { get; private set; }
        private string _baseUrl;

        private RouterService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (m, c, ch, e) => true,
                UseCookies = true
            };

            _httpClient = new HttpClient(handler);
        }

        public void Initialize(string ipAddress)
        {
            if (!ipAddress.StartsWith("http")) ipAddress = "https://" + ipAddress;
            if (!ipAddress.EndsWith("/")) ipAddress += "/";
            _baseUrl = ipAddress;
        }

        public async Task<bool> Authenticate(string username, string password, string endpointIp)
        {
            try
            {
                var loginPayload = PayloadFactory.CreateLoginPayload(username, password, endpointIp);

                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}ubus/session.login", loginPayload);

                if (response.IsSuccessStatusCode)
                {
                    string responseJsonAsString = await response.Content.ReadAsStringAsync();
                    CurrentSid = ParserHerlper.ParseSidFromJson(responseJsonAsString);
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Login failed: {ex.Message}");
            }

            return false;
        }

        public async Task<OntMetricsResponseModel> GetOntMetrics()
        {
            if (string.IsNullOrEmpty(CurrentSid)) return null;

            var payload = PayloadFactory.CreateOntInfoPayload(CurrentSid);
            string requestUrl = $"{_baseUrl}ubus/xlink.gpon.OntInformationGet";

            var response = await _httpClient.PostAsJsonAsync(requestUrl, payload);
            var json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);
            var dataProp = doc.RootElement.GetProperty("result")[1].GetProperty("data");

            return new OntMetricsResponseModel
            {
                TxPower = dataProp.GetProperty("TxOpticalPower").GetString(),
                RxPower = dataProp.GetProperty("RxOpticalPower").GetString(),
                Temperature = dataProp.GetProperty("WorkingTemperature").GetInt32(),
                Voltage = dataProp.GetProperty("WorkingVoltage").GetInt32(),
                BiasCurrent = dataProp.GetProperty("BiasCurrent").GetInt32(),
                Status = dataProp.GetProperty("ONTRegistraionStatus").GetString()
            };
        }
    }
}

namespace PlayFiberLogger.Models.Responses
{
    public class OntMetricsResponseModel
    {
        public string TxPower { get; set; }
        public string RxPower { get; set; }
        public int Temperature { get; set; }
        public int Voltage { get; set; }
        public int BiasCurrent { get; set; }
        public string Status { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}

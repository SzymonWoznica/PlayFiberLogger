using ClosedXML.Excel;
using PlayFiberLogger.Models.Responses;
using System.IO;

namespace PlayFiberLogger.Services
{

    internal class ExcelService
    {
        private readonly string _filePath;

        public ExcelService(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            InitializeFile();
        }
        private void InitializeFile()
        {
            if (!File.Exists(_filePath))
            {
                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Pomiary");

                // Headers
                worksheet.Cell(1, 1).Value = "Data i Czas";
                worksheet.Cell(1, 2).Value = "Rx Power [dBm]";
                worksheet.Cell(1, 3).Value = "Tx Power [dBm]";
                worksheet.Cell(1, 4).Value = "Temperatura [°C]";
                worksheet.Cell(1, 5).Value = "Napięcie [mV]";
                worksheet.Cell(1, 6).Value = "Status";

                // Header format
                worksheet.Range("A1:F1").Style.Font.Bold = true;
                worksheet.Range("A1:F1").Style.Fill.BackgroundColor = XLColor.LightGray;

                workbook.SaveAs(_filePath);
            }
        }
        public void AddEntry(OntMetricsResponseModel metrics)
        {
            try
            {
                using var workbook = new XLWorkbook(_filePath);
                var worksheet = workbook.Worksheet(1);

                // Find first empty row
                int lastRow = worksheet.LastRowUsed()?.RowNumber() ?? 0;
                int nextRow = lastRow + 1;

                worksheet.Cell(nextRow, 1).Value = metrics.Timestamp.ToString("yyyy-MM-dd HH:mm:ss");
                worksheet.Cell(nextRow, 2).Value = metrics.RxPower;
                worksheet.Cell(nextRow, 3).Value = metrics.TxPower;
                worksheet.Cell(nextRow, 4).Value = metrics.Temperature;
                worksheet.Cell(nextRow, 5).Value = metrics.Voltage;
                worksheet.Cell(nextRow, 6).Value = metrics.Status;

                workbook.Save();
            }
            catch (IOException)
            {
                return; 
            }

        }
    }
}

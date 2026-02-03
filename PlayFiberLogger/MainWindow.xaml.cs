using PlayFiberLogger.Models.Responses;
using PlayFiberLogger.Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PlayFiberLogger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool _isMonitoring = false;
        private CancellationTokenSource _cts;
        private ExcelService _excelService;

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            #region Default setup for start
            
            _isMonitoring = true;
            _cts = new CancellationTokenSource();
            ToggleUi(false);
            _excelService = new ExcelService("PlayFiberLogs.xlsx");

            #endregion

            #region Assign values from labels to variables

            string ip = IpAddressInput.Text.Trim();
            string username = LoginInput.Text.Trim();
            string password = PasswordInput.Password;

            #endregion
            
            // Sampling rate
            if (!int.TryParse(IntervalInput.Text, out int seconds)) seconds = 5;
            
            // Validation of required empty fields
            if (string.IsNullOrEmpty(ip) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Uzupełnij wszystkie pola!");
                _isMonitoring = false;
                ToggleUi(true);
                return;
            }


            try
            {
                StatusLabel.Text = "Próba logowania...";

                var router = RouterService.Instance;
                router.Initialize(ip);

                bool success = await router.Authenticate(username, password, ip);

                if (await router.Authenticate(username, password, ip))
                {
                    SidDisplay.Text = router.CurrentSid;
                    StatusLabel.Text = "Zalogowano!";
                    StatusLabel.Foreground = System.Windows.Media.Brushes.Green;

                    // Main request metrics loop 
                    while (!_cts.Token.IsCancellationRequested)
                    {
                        var metrics = await router.GetOntMetrics();

                        this.DisplayCurrentMetrics(metrics);
                        _excelService.AddEntry(metrics);

                        await Task.Delay(TimeSpan.FromSeconds(seconds), _cts.Token);
                    }
                }
                else
                {
                    StatusLabel.Text = "Błąd: Niepoprawne dane.";
                    StatusLabel.Foreground = System.Windows.Media.Brushes.Red;
                }
            }
            // Logic after click 'Stop'
            catch (OperationCanceledException)
            {
                StatusLabel.Text = "Status: Zatrzymano przez użytkownika.";
            }
            
            // Other exceptions
            catch (Exception ex)
            {
                StatusLabel.Text = "Błąd połączenia.";
                MessageBox.Show($"Szczegóły: {ex.Message}");
            }

            finally
            {
                ToggleUi(!_isMonitoring);
            }
        }

        private void DisplayCurrentMetrics(OntMetricsResponseModel meticsModel)
        {
            if (meticsModel != null)
            {
                RxPowerLabel.Text = $"{meticsModel.RxPower} dBm";
                TxPowerLabel.Text = $"{meticsModel.TxPower} dBm";
                TempLabel.Text = $"{meticsModel.Temperature} °C";
                VoltageLabel.Text = $"{(decimal)meticsModel.Voltage / 1000m} V";
                BiasLabel.Text = $"{meticsModel.BiasCurrent} mA";
                StatusOntLabel.Text = meticsModel.Status;

                StatusLabel.Text = $"Ostatni odczyt: {meticsModel.Timestamp:HH:mm:ss}";
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _cts?.Cancel();
            _isMonitoring = false;
            ToggleUi(true);
        }

        private void ToggleUi(bool isEnabled)
        {
            ModelSelector.IsEnabled = isEnabled;
            IpAddressInput.IsEnabled = isEnabled;
            LoginInput.IsEnabled = isEnabled;
            PasswordInput.IsEnabled = isEnabled;
            StartButton.IsEnabled = isEnabled;
            StopButton.IsEnabled = !isEnabled;

            RxPowerLabel.Text = "-- dBm";
            TxPowerLabel.Text = "-- dBm";
            TempLabel.Text = "-- °C";
            VoltageLabel.Text = "-- V";
            BiasLabel.Text = "-- mA";
            StatusOntLabel.Text = "Brak danych";
        }
    }
}
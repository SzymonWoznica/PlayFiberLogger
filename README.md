# Play Router Fiber Monitor (NP6089G) 📡

---

## [ENGLISH]

## 📸 Interface
<img width="418" height="719" alt="Application screenshot" src="https://github.com/user-attachments/assets/cdf04b0c-5872-400a-ba89-4d4fa1b30d04" />


A desktop diagnostic tool for real-time monitoring and logging fiber optic parameters from Play (Poland) routers (model NP6089G). Built with .NET 8 and WPF.


### ✨ Features

- **Real-time Monitoring**: Tracks Rx/Tx Optical Power, Temperature, Working Voltage, and Bias Current.
- **Automated Logging**: Saves all telemetry data directly to Excel (.xlsx) files for long-term analysis.
- **Smart Re-authentication**: Automatically handles session timeouts (ubus SID expiration).

### 🚀 Technologies

- **.NET 8.0 (WPF)**
- **ClosedXML**: High-performance Excel generation without Interop.
- **System.Text.Json**: Fast and secure UBUS protocol parsing.

### 🛠️ How to use

1. Run the application and select your router model (currently `NP6089G`).
2. Enter the router's IP address and your admin credentials.
3. Set the refresh interval (in seconds).
4. Click **START** to begin monitoring.
5. All data is automatically saved to a `.xlsx` file in the application folder.

### 📝 Planned Features

- [ ] **Email Alerts**: Send automated email notifications when signal levels (Rx/Tx) or temperature exceed safe thresholds.
- [ ] **Multi-model support**: Expansion of the internal database to support more router models.

---

## [POLISH]

## 📸 Interface
<img width="418" height="719" alt="Application screenshot" src="https://github.com/user-attachments/assets/cdf04b0c-5872-400a-ba89-4d4fa1b30d04" />


Narzędzie diagnostyczne do monitorowania i logowania parametrów światłowodu w czasie rzeczywistym z routerów sieci Play (modelu NP6089G). Aplikacja zbudowana w technologii .NET 8 i WPF.

### ✨ Funkcje

- **Monitorowanie w czasie rzeczywistym**: Śledzenie mocy optycznej Rx/Tx, temperatury, napięcia pracy oraz prądu wejściowego.
- **Automatyczne logowanie**: Zapisywanie wszystkich danych telemetrycznych bezpośrednio do plików Excel (.xlsx).
- **Inteligentna re-autoryzacja**: Automatyczna obsługa wygasania sesji (tokena SID protokołu ubus).

### 🚀 Technologie

- **.NET 8.0 (WPF)**
- **ClosedXML**: Wydajne generowanie plików Excel bez potrzeby instalacji pakietu Office.
- **System.Text.Json**: Szybkie i bezpieczne parsowanie protokołu UBUS.

### 🛠️ Instrukcja obsługi

1. Uruchom aplikację i wybierz model routera (obecnie `NP6089G`).
2. Wpisz adres IP routera oraz dane logowania administratora.
3. Ustaw interwał odświeżania (w sekundach).
4. Kliknij **START**, aby rozpocząć zbieranie danych.
5. Dane są automatycznie zapisywane do pliku `.xlsx` w folderze z aplikacją.

### 📝 Plany na przyszłość

- [ ] **Powiadomienia E-mail**: Automatyczne wysyłanie ostrzeżeń, gdy poziomy sygnału (Rx/Tx) lub temperatura przekroczą bezpieczne zakresy.
- [ ] **Obsługa wielu modeli**: Rozszerzenie bazy o kolejne modele routerów.

---

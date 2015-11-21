using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Windows.Networking;
using Windows.Networking.Sockets;

namespace jcMPP.UWP.ViewModels {
    public class MainPageModel : BaseModel {
        #region MVVM Properties

        private string _hostName;

        public string HostName
        {
            get { return _hostName; }
            set
            {
                _hostName = value;
                OnPropertyChanged();

                Enabled_btnStartScan = IsFormValid;
            }
        }

        private bool IsFormValid => !string.IsNullOrEmpty(HostName) && SelectedPorts.Any();

        private ObservableCollection<string> _ports;

        public ObservableCollection<string> Ports
        {
            get { return _ports; }
            set
            {
                _ports = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _selectedPorts;

        public ObservableCollection<string> SelectedPorts
        {
            get { return _selectedPorts; }
            set
            {
                _selectedPorts = value;
                OnPropertyChanged();

                Enabled_btnStartScan = IsFormValid;
            }
        }

        private ObservableCollection<string> _scanResults;

        public ObservableCollection<string> ScanResults
        {
            get { return _scanResults; }
            set
            {
                _scanResults = value;
                OnPropertyChanged();
            }
        }

        private bool _enabled_btnStartScan;

        public bool Enabled_btnStartScan {
            get { return _enabled_btnStartScan; }
            set { _enabled_btnStartScan = value; OnPropertyChanged(); }
        }

        #endregion

        public MainPageModel() {
            HideRunning();

            Ports = new ObservableCollection<string> { "21", "22", "25", "80" };

            Enabled_btnStartScan = IsFormValid;
        }

        public async Task<bool> RunScan() {
            ShowRunning();

            ScanResults = new ObservableCollection<string>();

            using (var streamSocket = new StreamSocket()) {
                foreach (var port in SelectedPorts) {
                    var isSuccessful = true;

                    try {
                        await streamSocket.ConnectAsync(new HostName(HostName), port);
                    } catch (Exception ex) {
                        switch (SocketError.GetStatus(ex.HResult)) {
                            case SocketErrorStatus.HostNotFound:
                                break;
                            case SocketErrorStatus.HostIsDown:
                                break;
                        }

                        isSuccessful = false;
                    }

                    ScanResults.Add($"Port {port} is {(isSuccessful ? "open" : "closed")}");
                }
            }

            HideRunning();

            return true;
        }
    }
}
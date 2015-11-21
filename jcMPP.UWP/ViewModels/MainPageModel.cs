using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Windows.Networking;
using Windows.Networking.Sockets;
using jcMPP.PCL.Objects;

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

        private bool IsFormValid => !string.IsNullOrEmpty(HostName) && SelectedPorts != null && SelectedPorts.Any();

        private ObservableCollection<PortListingItem> _ports;

        public ObservableCollection<PortListingItem> Ports
        {
            get { return _ports; }
            set
            {
                _ports = new ObservableCollection<PortListingItem>(value.OrderBy(a => a.PortNumber));
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PortListingItem> _selectedPorts;

        public ObservableCollection<PortListingItem> SelectedPorts
        {
            get { return _selectedPorts; }
            set
            {
                _selectedPorts = value;
                OnPropertyChanged();

                Enabled_btnStartScan = IsFormValid;
            }
        }

        private ObservableCollection<PortScanListingItem> _scanResults;

        public ObservableCollection<PortScanListingItem> ScanResults
        {
            get { return _scanResults; }
            set
            {
                _scanResults = value;
                OnPropertyChanged();
            }
        }

        private bool _enabled_btnStartScan;

        public bool Enabled_btnStartScan
        {
            get { return _enabled_btnStartScan; }
            set { _enabled_btnStartScan = value; OnPropertyChanged(); }
        }

        private bool _enabled_btnShareResults;

        public bool Enabled_btnShareResults
        {
            get { return _enabled_btnShareResults; }
            set { _enabled_btnShareResults = value; OnPropertyChanged(); }
        }
        #endregion

        public MainPageModel() {
            HideRunning();

            Ports = new ObservableCollection<PortListingItem> {
                new PortListingItem("FTP", 21),
                new PortListingItem("HTTP", 80),
                new PortListingItem("SMTP", 25)
            };

            Enabled_btnStartScan = IsFormValid;
            Enabled_btnShareResults = false;
        }

        public string ScanResultForShare {
            get {
                var str = "Port Description\tPort #\tOpen?" + "<br/>";

                foreach (var result in ScanResults) {
                    str += $"{result.Description}\t{result.PortNumber}\t{result.IsOpen}" + "<br/>";
                }

                return str;
            }
        }

        public async Task<bool> RunScan() {
            ShowRunning();

            ScanResults = new ObservableCollection<PortScanListingItem>();

            using (var streamSocket = new StreamSocket()) {
                foreach (var portItem in SelectedPorts) {
                    var isSuccessful = true;

                    try {
                        await streamSocket.ConnectAsync(new HostName(HostName), portItem.PortNumber.ToString());
                    } catch (Exception ex) {
                        switch (SocketError.GetStatus(ex.HResult)) {
                            case SocketErrorStatus.HostNotFound:
                                break;
                            case SocketErrorStatus.HostIsDown:
                                break;
                        }

                        isSuccessful = false;
                    }

                    ScanResults.Add(new PortScanListingItem(portItem, isSuccessful));
                }
            }

            Enabled_btnShareResults = ScanResults.Any();

            HideRunning();

            return true;
        }
    }
}
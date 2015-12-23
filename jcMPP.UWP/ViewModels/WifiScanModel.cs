using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Windows.Devices.WiFi;

using jcMPP.PCL.Enums;
using jcMPP.PCL.PlatformAbstractions;
using jcMPP.UWP.PlatformImplementations;

namespace jcMPP.UWP.ViewModels {
    public class WifiScanModel : BaseModel {
        private WiFiAdapter _adapter;

        private bool _Enabled_btnRefresh;

        public bool Enabled_btnRefresh {
            get { return _Enabled_btnRefresh; }

            set { _Enabled_btnRefresh = value; OnPropertyChanged(); }
        }

        private ObservableCollection<WiFiAvailableNetwork> _wifiNetworks;

        public ObservableCollection<WiFiAvailableNetwork> WifiNetworks {
            get { return _wifiNetworks; }

            set { _wifiNetworks = value; OnPropertyChanged(); }
        }

        public WifiScanModel(BaseFileIO baseFileIO) : base(baseFileIO) { }

        public WifiScanModel() : base(new UWPFileIO()) { }

        public async Task<WiFiScanResultTypes> LoadData() {
            Enabled_btnRefresh = false;

            ShowRunning();
            
            WifiNetworks = new ObservableCollection<WiFiAvailableNetwork>();

            var access = await WiFiAdapter.RequestAccessAsync();

            if (access != WiFiAccessStatus.Allowed) {
                Enabled_btnRefresh = true;

                HideRunning();

                return WiFiScanResultTypes.NO_ACCESS_TO_WIFI_CARD;
            }

            var result = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(WiFiAdapter.GetDeviceSelector());

            if (result.Count > 0) {
                _adapter = await WiFiAdapter.FromIdAsync(result[0].Id);
            } else {
                Enabled_btnRefresh = true;
                HideRunning();

                return WiFiScanResultTypes.NO_WIFI_CARD;
            }

            await _adapter.ScanAsync();

            foreach (var network in _adapter.NetworkReport.AvailableNetworks) {
                WifiNetworks.Add(network);
            }

            WifiNetworks = new ObservableCollection<WiFiAvailableNetwork>(WifiNetworks.OrderByDescending(a => a.SignalBars));

            Enabled_btnRefresh = true;

            HideRunning();

            return WiFiScanResultTypes.SUCCESS;
        }
    }
}
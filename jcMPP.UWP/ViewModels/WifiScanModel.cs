using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.WiFi;
using Windows.Networking.Connectivity;
using jcMPP.PCL.PlatformAbstractions;

namespace jcMPP.UWP.ViewModels {
    public class WifiScanModel : BaseModel {
        private WiFiAdapter _adapter;

        private ObservableCollection<WiFiAvailableNetwork> _wifiNetworks;

        public ObservableCollection<WiFiAvailableNetwork> WifiNetworks
        {
            get { return _wifiNetworks; }

            set { _wifiNetworks = value; OnPropertyChanged(); }
        }

        public WifiScanModel(BaseFileIO baseFileIO) : base(baseFileIO) {
        }

        public async Task<bool> LoadData() {
            WifiNetworks = new ObservableCollection<WiFiAvailableNetwork>();

            var access = await WiFiAdapter.RequestAccessAsync();

            if (access != WiFiAccessStatus.Allowed) {
                return false;
            }

            var result = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(WiFiAdapter.GetDeviceSelector());

            if (result.Count > 0) {
                _adapter = await WiFiAdapter.FromIdAsync(result[0].Id);
            }

            await _adapter.ScanAsync();

            foreach (var network in _adapter.NetworkReport.AvailableNetworks) {
                WifiNetworks.Add(network);
            }

            WifiNetworks = new ObservableCollection<WiFiAvailableNetwork>(WifiNetworks.OrderByDescending(a => a.SignalBars));

            return true;
        }
    }
}
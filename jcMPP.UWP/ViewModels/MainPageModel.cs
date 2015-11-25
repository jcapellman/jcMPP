using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Windows.Networking;
using Windows.Networking.Sockets;
using jcMPP.PCL.Enums;
using jcMPP.PCL.Objects;
using jcMPP.PCL.Handlers;
using jcMPP.PCL.Objects.AssetTypeWrappers;
using jcMPP.PCL.Objects.Ports;
using jcMPP.PCL.PlatformAbstractions;

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

        private bool _enabled_ScanResults;

        public bool Enabled_ScanResults
        {
            get { return _enabled_ScanResults; }
            set { _enabled_ScanResults = value; OnPropertyChanged(); }
        }

        #endregion

        private readonly BaseFileIO _baseFileIO;

        public MainPageModel(BaseFileIO baseFileIO) {
            HideRunning();

            _baseFileIO = baseFileIO;

            Enabled_btnStartScan = IsFormValid;
            Enabled_btnShareResults = false;
            Enabled_ScanResults = false;
        }

        public async Task<bool> LoadData() {
            var portDefinition = await _baseFileIO.GetFile<PortDefinitionsResponseItem>(ASSET_TYPES.PORT_DEFINITIONS);

            if (portDefinition.HasError) {
                return false;
            }

            Ports = new ObservableCollection<PortListingItem>(portDefinition.Value.Ports);

            return true;
        }

        public string ScanResultForShare
        {
            get
            {
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
            Enabled_ScanResults = ScanResults.Any();

            HideRunning();

            return true;
        }

        public async Task<bool> UpdateDefinitionFiles() {
            var fileHandler = new FileHandler();

            var clientList = await _baseFileIO.GetAllClientFiles();

            var files = await fileHandler.GetFiles(clientList);

            await _baseFileIO.WriteFile(ASSET_TYPES.FILE_LIST, files.Select(a => a.ID).ToList());

            foreach (var file in files) {
                switch ((ASSET_TYPES)file.AssetTypeID) {
                    case ASSET_TYPES.PORT_DEFINITIONS:
                        await _baseFileIO.WriteFile(ASSET_TYPES.PORT_DEFINITIONS, file.Content);
                        break;
                }
            }

            return true;
        }
    }
}
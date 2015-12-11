using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Windows.UI.Xaml;
using jcMPP.PCL.Enums;
using jcMPP.PCL.Handlers;
using jcMPP.PCL.PlatformAbstractions;

namespace jcMPP.UWP.ViewModels {
    public class BaseModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Visibility _progressBarRunning;

        public Visibility ProgressBarRunning { get { return _progressBarRunning; ; } set { _progressBarRunning = value; OnPropertyChanged(); } }

        protected void ShowRunning() {
            ProgressBarRunning = Visibility.Visible;
        }

        protected void HideRunning() {
            ProgressBarRunning = Visibility.Collapsed;
        }

        protected readonly BaseFileIO _baseFileIO;

        public BaseModel(BaseFileIO baseFileIO)
        {
            _baseFileIO = baseFileIO;
        }

        public bool HasInternetConnection {
            get {
                var result = NetworkInformation.GetInternetConnectionProfile();

                return result != null;
            }
        }

        public async Task<DefinitionResultTypes> UpdateDefinitionFiles() {
            if (!HasInternetConnection) {
                return DefinitionResultTypes.NO_INTERNET;
            }

            try {
                var fileHandler = new FileHandler();

                var clientList = await _baseFileIO.GetAllClientFiles();

                var files = await fileHandler.GetFiles(clientList);

                if (!files.Value.Any()) {
                    return DefinitionResultTypes.NO_UPDATE_NEEDED;
                }

                var writeResult = await _baseFileIO.WriteFile(ASSET_TYPES.FILE_LIST, files.Value.Select(a => a.ID).ToList());

                if (!writeResult.Value) {
                    throw new Exception("Could not write file databsae");
                }

                foreach (var file in files.Value) {
                    switch ((ASSET_TYPES)file.AssetTypeID) {
                        case ASSET_TYPES.PORT_DEFINITIONS:
                            await _baseFileIO.WriteFile(ASSET_TYPES.PORT_DEFINITIONS, file.Content);
                            break;
                    }
                }

                return DefinitionResultTypes.UPDATE_SUCCESFULL;
            } catch (Exception ex) {
                var str = ex;
                return DefinitionResultTypes.CANT_FIND_DEFINITION_SERVER;
            }
        }
    }
}
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using jcMPP.PCL.Enums;
using jcMPP.PCL.Objects.KeepAlive;
using jcMPP.PCL.PlatformAbstractions;
using jcMPP.UWP.PlatformImplementations;

namespace jcMPP.UWP.ViewModels {
    public class KeepAliveListingModel : BaseModel {
        public KeepAliveListingModel() : base(new UWPFileIO()) { }

        public KeepAliveListingModel(BaseFileIO baseFileIO) : base(baseFileIO) { }

        private ObservableCollection<KeepAliveListingItem> _keepAliveListing;

        public ObservableCollection<KeepAliveListingItem> KeepAliveListing {
            get { return _keepAliveListing; }
            set { _keepAliveListing = value; OnPropertyChanged(); }
        }  

        public async Task<bool> LoadListing() {
            ShowRunning();

            var result = await _baseFileIO.GetFile<List<KeepAliveListingItem>>(ASSET_TYPES.KEEP_ALIVE_LISTING);

            KeepAliveListing = result.Value == null ? new ObservableCollection<KeepAliveListingItem>() : new ObservableCollection<KeepAliveListingItem>(result.Value);

            HideRunning();

            return true;
        }
    }
}
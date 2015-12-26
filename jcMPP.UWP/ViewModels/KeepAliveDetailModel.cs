using System.Threading.Tasks;

using jcMPP.PCL.Enums;
using jcMPP.PCL.Objects.KeepAlive;
using jcMPP.PCL.PlatformAbstractions;
using jcMPP.UWP.PlatformImplementations;

namespace jcMPP.UWP.ViewModels {
    public class KeepAliveDetailModel : BaseModel {
        private KeepAliveItem _item;

        public KeepAliveItem Item {  get { return _item; } set { _item = value; OnPropertyChanged(); } }

        public KeepAliveDetailModel(BaseFileIO baseFileIO) : base(baseFileIO) { }

        public KeepAliveDetailModel() : base(new UWPFileIO()) { }

        public async Task<bool> LoadData() {
            ShowRunning();

            var data = await _baseFileIO.GetFile<KeepAliveItem>(ASSET_TYPES.KEEP_ALIVE_ITEM);

            if (!data.HasError) {
                Item = data.Value;
            }

            HideRunning();

            return data.Value != null;
        }
    }
}
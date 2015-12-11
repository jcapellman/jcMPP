using System.Threading.Tasks;

using jcMPP.PCL.PlatformAbstractions;

namespace jcMPP.UWP.ViewModels {
    public class SettingsModel : BaseModel {
        public SettingsModel(BaseFileIO baseFileIO) : base(baseFileIO) {
            HideRunning();
        }

        public async Task<bool> ClearFiles() {
            return await _baseFileIO.ClearFiles();
        }
    }
}
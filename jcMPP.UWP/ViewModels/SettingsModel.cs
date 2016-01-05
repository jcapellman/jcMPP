using System;
using System.Threading.Tasks;
using jcMPP.PCL.Enums;
using jcMPP.PCL.Objects;
using jcMPP.PCL.PlatformAbstractions;

namespace jcMPP.UWP.ViewModels {
    public class SettingsModel : BaseModel {
        private bool _setting_EnableRoaming;

        public bool Setting_EnableRoaming
        {
            get { return _setting_EnableRoaming; }
            set { _setting_EnableRoaming = value; OnPropertyChanged(); }
        }

        public SettingsModel(BaseFileIO baseFileIO) : base(baseFileIO) {
            HideRunning();
        }

        public void LoadData() {
            ShowRunning();

            Setting_EnableRoaming = App.AppSetting.GetValue<bool>(SettingKeys.ENABLE_ROAMING_SETTINGS);

            HideRunning();
        }

        public CTO<bool> SaveSettings() {
            try {
                App.AppSetting.SetValue(SettingKeys.ENABLE_ROAMING_SETTINGS, Setting_EnableRoaming);

                return new CTO<bool>(true);
            } catch (Exception ex) {
                return new CTO<bool>(false, ex);
            }
        }

        public async Task<CTO<bool>> ClearFiles() {
            return await _baseFileIO.ClearFiles();
        }
    }
}
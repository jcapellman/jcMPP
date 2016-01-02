using System;

using Windows.Storage;

using jcMPP.PCL.Enums;
using jcMPP.PCL.PlatformAbstractions;

namespace jcMPP.UWP.Library.PlatformImplementations {
    public class UWPSetting : BaseSetting {
        private static ApplicationDataContainer CurrentSettingsContainer => ApplicationData.Current.RoamingSettings;

        public override T GetValue<T>(SettingKeys key) {
            var objectValue = CurrentSettingsContainer.Values[key.ToString()];

            return (T)Convert.ChangeType(objectValue, typeof (T));
        }

        public override void SetValue(SettingKeys key, object value) {
            CurrentSettingsContainer.Values[key.ToString()] = value;
        }
    }
}
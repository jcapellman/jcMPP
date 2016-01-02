using System;

using jcMPP.PCL.Enums;

namespace jcMPP.PCL.PlatformAbstractions {
    public abstract class BaseSetting : BasePA {
        public abstract T GetValue<T>(SettingKeys key);

        public abstract void SetValue(SettingKeys key, object value);

        protected T GetDefaultValue<T>(SettingKeys key) {
            object objectValue = null;

            switch (key) {
                 case SettingKeys.ENABLE_ROAMING_SETTINGS:
                    objectValue = true;
                    break;
            }

            return (T)Convert.ChangeType(objectValue, typeof (T));
        }
    }
}
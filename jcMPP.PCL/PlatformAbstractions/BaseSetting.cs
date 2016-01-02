using jcMPP.PCL.Enums;

namespace jcMPP.PCL.PlatformAbstractions {
    public abstract class BaseSetting : BasePA {
        public abstract T GetValue<T>(SettingKeys key);

        public abstract void SetValue(SettingKeys key, object value);
    }
}
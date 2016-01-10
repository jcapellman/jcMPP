using Windows.ApplicationModel;

using jcMPP.PCL.PlatformAbstractions;

namespace jcMPP.UWP.ViewModels {
    public class AboutModel : BaseModel {
        public AboutModel(BaseFileIO baseFileIO) : base(baseFileIO) { }

        private string _versionString;

        public string VersionString {
            get { return _versionString; }

            set { _versionString = value; OnPropertyChanged(); }
        }

        public void LoadData() {
            var version = Package.Current.Id.Version;

            VersionString = $"Version {version.Major}.{version.Minor}.{version.Revision}.{version.Build}";
        }
    }
}
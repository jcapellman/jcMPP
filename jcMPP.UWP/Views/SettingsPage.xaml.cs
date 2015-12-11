using Windows.UI.Xaml;

using jcMPP.UWP.PlatformImplementations;
using jcMPP.UWP.ViewModels;

namespace jcMPP.UWP.Views {
    public sealed partial class SettingsPage {

        public SettingsPage() {
            this.InitializeComponent();

            DataContext = new SettingsModel(new UWPFileIO());
        }

        private async void btnClearFiles_OnClick(object sender, RoutedEventArgs e) {
            var result = await getVuewModel<SettingsModel>().ClearFiles();

            ShowDialog(result ? "Cleared all files successfully" : "Failed to clear files");
        }

        private async void BtnCheckForUpdates_OnClick(object sender, RoutedEventArgs e) {
            var result = await CheckForUpdatedDefinitions<SettingsModel>();
        }

        public override T getVuewModel<T>()
        {
            return (T) DataContext;
        }
    }
}
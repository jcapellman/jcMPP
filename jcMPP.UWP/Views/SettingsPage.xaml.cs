using Windows.UI.Xaml;

using jcMPP.UWP.PlatformImplementations;
using jcMPP.UWP.ViewModels;

namespace jcMPP.UWP.Views {
    public sealed partial class SettingsPage {

        public SettingsPage() : base(typeof(SettingsModel), new UWPFileIO()) {
            this.InitializeComponent();
        }

        private async void btnClearFiles_OnClick(object sender, RoutedEventArgs e) {
            var result = await viewModel<SettingsModel>().ClearFiles();

            ShowDialog(result ? "Cleared all files successfully" : "Failed to clear files");
        }

        private async void BtnCheckForUpdates_OnClick(object sender, RoutedEventArgs e) {
            var result = await CheckForUpdatedDefinitions();
        }
    }
}